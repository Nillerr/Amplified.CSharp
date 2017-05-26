using System;
using System.Collections.Generic;
using Amplified.CSharp.Internal;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
    /// <summary>
    ///     Maybe represents the possibility of a value being present. It is equivalent to <c>null</c> for reference
    ///     types, but being a <c>struct</c>, it can never be <c>null</c> itself (unless it is boxed).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Maybe<T> : IEquatable<Maybe<T>>, IEquatable<Some<T>>, IEquatable<None>, ICanBeNone
    {
        public static Maybe<T> None = default(Maybe<T>);
        
        private readonly T _value;

        public Maybe(T value) : this()
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _value = value;
            IsSome = true;
        }

        [Pure]
        public static implicit operator Maybe<T>(None none)
        {
            return default(Maybe<T>);
        }

        [Pure]
        public bool IsSome { get; }
        
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
        public bool Equals(Maybe<T> other)
        {
            return (IsNone && other.IsNone) ||
                   (IsSome && other.IsSome && EqualityComparer<T>.Default.Equals(_value, other._value));
        }

        [Pure]
        public bool Equals(Some<T> other)
        {
            return IsSome && EqualityComparer<T>.Default.Equals(_value, other.Value);
        }

        [Pure]
        public bool Equals(None other)
        {
            return IsNone;
        }

        [Pure]
        public bool Equals([NotNull] ICanBeNone other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            
            return IsNone && other.IsNone;
        }

        [Pure]
        public override bool Equals([CanBeNull] object obj)
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
        public static bool operator ==(Maybe<T> left, None right)
        {
            return left.Equals(right);
        }

        [Pure]
        public static bool operator !=(Maybe<T> left, None right)
        {
            return !left.Equals(right);
        }

        [Pure]
        public static bool operator ==(Maybe<T> left, Some<T> right)
        {
            return left.Equals(right);
        }

        [Pure]
        public static bool operator !=(Maybe<T> left, Some<T> right)
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

    public static class Maybe
    {
        public static Maybe<T> Some<T>([NotNull] T value) => new Maybe<T>(value);

        public static None None() => default(None);

        public static Maybe<T> OfNullable<T>([CanBeNull] T value) where T : class
        {
            return value == null ? Maybe<T>.None : new Maybe<T>(value);
        }

        public static Maybe<T> OfNullable<T>([CanBeNull] T? value) where T : struct
        {
            return value.HasValue ? new Maybe<T>(value.Value) : Maybe<T>.None;
        }
    }
}
