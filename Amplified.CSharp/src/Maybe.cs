using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Amplified.CSharp.Internal;
using Amplified.CSharp.Internal.Extensions;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
    /// <summary>
    ///     Maybe represents the possibility of a value being present. It is equivalent to <c>null</c> for reference
    ///     types, but being a <c>struct</c>, it can never be <c>null</c> itself (unless it is boxed).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerStepThrough]
    public struct Maybe<T> : IMaybe, IEquatable<Maybe<T>>
    {
        /// <summary>
        /// Returns a <c>None</c> instance (a <see cref="Maybe{T}"/> without a value).
        /// </summary>
        /// <returns>A <see cref="Maybe{T}"/>.<c>None</c>.</returns>
        public static Maybe<T> None() => default(Maybe<T>);

        /// <summary>
        /// Creates a Some case using <see cref="value"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A <see cref="Maybe{T}"/>.<c>Some</c> with a value.</returns>
        public static Maybe<T> Some(T value)
        {
            return new Maybe<T>(value);
        }

        private readonly T _value;

        private Maybe(T value) : this()
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _value = value;
            IsSome = true;
        }

        /// <summary>
        /// Converts an instance of <see cref="None"/> to <see cref="Maybe{T}"/>.<c>None</c>.
        /// </summary>
        /// <remarks>
        /// This operator is required to support type-parameterless Maybe.None construction.
        /// </remarks>
        [Pure]
        public static implicit operator Maybe<T>(None none)
        {
            return default(Maybe<T>);
        }

        /// <summary>
        /// Returns <c>true</c> if the instance contains a value.
        /// </summary>
        [Pure]
        public bool IsSome { get; }
        
        /// <summary>
        /// Returns <c>false</c> if the instance does not contain a value.
        /// </summary>
        [Pure]
        public bool IsNone => IsSome == false;

        [Pure]
        public TResult Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<None, TResult> none
        )
        {
            return IsSome ? some(_value) : none(default(None));
        }

        [Pure]
        public TResult Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<TResult> none
        )
        {
            return IsSome ? some(_value) : none();
        }

        [Pure]
        public Unit Match(
            [InstantHandle, NotNull] Action<T> some,
            [InstantHandle, NotNull] Action<None> none
        )
        {
            if (IsSome)
                some(_value);
            else
                none(default(None));
            
            return default(Unit);
        }

        [Pure]
        public Unit Match(
            [InstantHandle, NotNull] Action<T> some,
            [InstantHandle, NotNull] Action none
        )
        {
            if (IsSome)
                some(_value);
            else
                none();
            
            return default(Unit);
        }

        [Pure]
        public bool Equals(Maybe<T> other)
        {
            return (IsNone && other.IsNone) ||
                   (IsSome && other.IsSome && EqualityComparer<T>.Default.Equals(_value, other._value));
        }

        [Pure]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return (obj is Maybe<T> && Equals((Maybe<T>) obj)) ||
                   (obj is None && Equals((None) obj)) ||
                   (obj is Some<T> && Equals((Some<T>) obj));
        }

        [Pure]
        public override int GetHashCode()
        {
            if (IsNone) return 0;
            
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ IsSome.GetHashCode();
            }
        }

        [Pure]
        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        [Pure]
        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !left.Equals(right);
        }
        
        [Pure]
        public override string ToString()
        {
            return Match(
                some => $"Some({some})",
                none => "None"
            );
        }
    }
    
    [DebuggerStepThrough]
    public static class Maybe
    {
        public static Maybe<T> Some<T>([NotNull] T value) => Maybe<T>.Some(value);
        
        public static None None() => default(None);

        public static Maybe<T> OfNullable<T>([CanBeNull] T value) where T : class
        {
            return value == null
                ? Maybe<T>.None()
                : Maybe<T>.Some(value);
        }

        public static Maybe<T> OfNullable<T>([CanBeNull] T? value) where T : struct
        {
            return value.HasValue ? Maybe<T>.Some(value.Value) : Maybe<T>.None();
        }

        public static AsyncMaybe<T> SomeAsync<T>([NotNull] T value) 
            => new AsyncMaybe<T>(Task.FromResult(Maybe<T>.Some(value)));
        
        public static AsyncMaybe<T> SomeAsync<T>([NotNull] Task<T> task) => new AsyncMaybe<T>(task.Then(Maybe<T>.Some));

        public static AsyncMaybe<T> OfNullableAsync<T>([NotNull] Task<T> task)
            where T : class
        {
            return new AsyncMaybe<T>(task.Then(OfNullable));
        }

        public static AsyncMaybe<T> OfNullableAsync<T>([NotNull] Task<T?> task)
            where T : struct
        {
            return new AsyncMaybe<T>(task.Then(OfNullable));
        }
    }
}