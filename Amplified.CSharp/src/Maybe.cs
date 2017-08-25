using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Amplified.CSharp.ComponentModel;
using Amplified.CSharp.Internal;
using Amplified.CSharp.Internal.Extensions;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
    /// <summary>
    ///   <para>
    ///     Maybe represents the possibility of a value being present. It is equivalent to <c>null</c> for reference
    ///     types, but being a <c>struct</c>, it can never be <c>null</c> itself (unless it is boxed).
    ///   </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [TypeConverter(typeof(MaybeConverter))]
    [DebuggerStepThrough]
    [DebuggerDisplay("{" + nameof(DebuggerDisplayString) + "}")]
    public struct Maybe<T> : IMaybe, IEquatable<Maybe<T>>
    {
        /// <summary>
        ///   <para>Returns a <c>None</c> instance (a <see cref="Maybe{T}"/> without a value).</para>
        /// </summary>
        /// <returns>A <see cref="Maybe{T}"/>.<c>None</c>.</returns>
        [Pure]
        public static Maybe<T> None() => default(Maybe<T>);

        /// <summary>
        ///   <para>Creates a Some case using <see cref="value"/>.</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A <see cref="Maybe{T}"/>.<c>Some</c> with a value.</returns>
        [Pure]
        public static Maybe<T> Some(T value)
        {
            return new Maybe<T>(value);
        }

        private string DebuggerDisplayString => Match(some => $"Some({some})", none => none.ToString());

        private readonly T _value;

        private Maybe(T value) : this()
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _value = value;
            IsSome = true;
        }

        /// <summary>
        ///   <para>Converts an instance of <see cref="None"/> to <see cref="Maybe{T}"/>.<c>None</c>.</para>
        /// </summary>
        /// <remarks>
        ///   <para>This operator is required to support type-parameterless Maybe.None construction.</para>
        /// </remarks>
        [Pure]
        public static implicit operator Maybe<T>(None none)
        {
            return default(Maybe<T>);
        }

        /// <summary>
        ///   <para>Returns <c>true</c> if the instance contains a value.</para>
        /// </summary>
        [Pure]
        public bool IsSome { get; }
        
        /// <summary>
        ///   <para>Returns <c>false</c> if the instance does not contain a value.</para>
        /// </summary>
        [Pure]
        public bool IsNone => IsSome == false;

        /// <summary>
        ///   <para>
        ///     Evaluates the value of the <c>Maybe</c>, resulting in either <paramref name="some"/> or 
        ///     <paramref name="none"/> being invoked, depending on the value of the <c>Maybe</c>.
        ///   </para>
        /// </summary>
        /// <param name="some">Function to invoke if the <c>Maybe</c> is <c>Some</c>.</param>
        /// <param name="none">Function to invoke if the <c>Maybe</c> is <c>None</c>.</param>
        /// <typeparam name="TResult">Type of result to return from the function arguments.</typeparam>
        /// <returns>The result of calling either <paramref name="some"/> or <paramref name="none"/>.</returns>
        [Pure]
        public TResult Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<None, TResult> none
        )
        {
            return IsSome ? some(_value) : none(default(None));
        }

        /// <summary>
        ///   <para>
        ///     Evaluates the value of the <c>Maybe</c>, resulting in either <paramref name="some"/> or 
        ///     <paramref name="none"/> being invoked, depending on the value of the <c>Maybe</c>.
        ///   </para>
        /// </summary>
        /// <param name="some">Function to invoke if the <c>Maybe</c> is <c>Some</c>.</param>
        /// <param name="none">Function to invoke if the <c>Maybe</c> is <c>None</c>.</param>
        /// <typeparam name="TResult">Type of result to return from the function arguments.</typeparam>
        /// <returns>The result of calling either <paramref name="some"/> or <paramref name="none"/>.</returns>
        [Pure]
        public TResult Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<TResult> none
        )
        {
            return IsSome ? some(_value) : none();
        }

        /// <summary>
        ///   <para>
        ///     Evaluates the value of the <c>Maybe</c>, resulting in either <paramref name="some"/> or 
        ///     <paramref name="none"/> being invoked, depending on the value of the <c>Maybe</c>.
        ///   </para>
        /// </summary>
        /// <param name="some">Action to invoke if the <c>Maybe</c> is <c>Some</c>.</param>
        /// <param name="none">Actiono to invoke if the <c>Maybe</c> is <c>None</c>.</param>
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

        /// <summary>
        ///   <para>
        ///     Evaluates the value of the <c>Maybe</c>, resulting in either <paramref name="some"/> or 
        ///     <paramref name="none"/> being invoked, depending on the value of the <c>Maybe</c>.
        ///   </para>
        /// </summary>
        /// <param name="some">Action to invoke if the <c>Maybe</c> is <c>Some</c>.</param>
        /// <param name="none">Actiono to invoke if the <c>Maybe</c> is <c>None</c>.</param>
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

        /// <summary>
        ///   <para>
        ///     Evaluates the value of the <c>Maybe</c>, invoking <paramref name="some"/> if the value is <c>Some</c>.
        ///   </para>
        /// </summary>
        /// <param name="some">Action to invoke if the <c>Maybe</c> is <c>Some</c>.</param>
        public Unit MatchSome([InstantHandle, NotNull] Action<T> some)
        {
            if (IsSome)
                some(_value);

            return default(Unit);
        }

        /// <summary>
        ///   <para>
        ///     Evaluates the value of the <c>Maybe</c>, invoking <paramref name="none"/> if the value is <c>None</c>.
        ///   </para>
        /// </summary>
        /// <param name="none">Action to invoke if the <c>Maybe</c> is <c>None</c>.</param>
        public Unit MatchNone([InstantHandle, NotNull] Action<None> none)
        {
            if (IsNone)
                none(default(None));

            return default(Unit);
        }

        /// <summary>
        ///   <para>
        ///     Evaluates the value of the <c>Maybe</c>, invoking <paramref name="none"/> if the value is <c>None</c>.
        ///   </para>
        /// </summary>
        /// <param name="none">Action to invoke if the <c>Maybe</c> is <c>None</c>.</param>
        public Unit MatchNone([InstantHandle, NotNull] Action none)
        {
            if (IsNone)
                none();

            return default(Unit);
        }

        /// <summary>
        ///   <para>
        ///     Indicates whether the current <c>Maybe</c> is equal to another <c>Maybe</c> of the same type.
        ///   </para>
        /// </summary>
        /// <param name="other">A <c>Maybe</c> to compare with this <c>Maybe</c>.</param>
        /// <remarks>
        ///   <para>
        ///     Two instances of <c>Maybe</c> are equal if they are both <c>None</c>, or both <c>Some</c> and their 
        ///     values are equal.
        ///   </para>
        /// </remarks>
        /// <returns>
        ///   <para>
        ///     <see langword="true"/> if the <c>Maybe</c> is equal to <paramref name="other"/> parameter; otherwise, 
        ///     <see langword="false"/>.
        ///   </para>
        /// </returns>
        [Pure]
        public bool Equals(Maybe<T> other)
        {
            return (IsNone && other.IsNone) ||
                   (IsSome && other.IsSome && EqualityComparer<T>.Default.Equals(_value, other._value));
        }

        /// <summary>
        ///   <para>
        ///     Indicates whether the current <c>Maybe</c> is equal to another <c>Maybe</c> of the same type, or an 
        ///     instance of <c>None</c>.
        ///   </para>
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <remarks>
        ///   <para>
        ///     Two instances of <c>Maybe</c> are equal if they are both <c>None</c>, or both <c>Some</c> and their 
        ///     values are equal. When comparing a <c>Maybe</c> to <c>None</c>, they are equal if the <c>Maybe</c> is 
        ///     None.
        ///   </para>
        /// </remarks>
        /// <returns>
        ///   <para>
        ///     <see langword="true"/> if the <c>Maybe</c> is equal to <paramref name="obj"/> parameter; otherwise, 
        ///     <see langword="false"/>.
        ///   </para>
        /// </returns>
        [Pure]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return (obj is Maybe<T> && Equals((Maybe<T>) obj)) ||
                   (obj is None && Equals((None) obj));
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        [Pure]
        public override int GetHashCode()
        {
            if (IsNone) return 0;
            
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ IsSome.GetHashCode();
            }
        }

        /// <summary>
        ///   <para>Indicates whether two instances of <c>Maybe</c> are equal.</para>
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     Two instances of <c>Maybe</c> are equal if they are both <c>None</c>, or both <c>Some</c> and their 
        ///     values are equal.
        ///   </para>
        /// </remarks>
        /// <returns>
        ///   <para><see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</para>
        /// </returns>
        [Pure]
        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///   <para>Indicates whether two instances of <c>Maybe</c> are not equal.</para>
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     Two instances of <c>Maybe</c> are equal if they are both <c>None</c>, or both <c>Some</c> and their 
        ///     values are equal.
        ///   </para>
        /// </remarks>
        /// <returns>
        ///   <para><see langword="true"/> if the instances are not equal; otherwise, <see langword="false"/>.</para>
        /// </returns>
        [Pure]
        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !left.Equals(right);
        }
        
        [Pure]
        public override string ToString()
        {
            return Match(
                some => some.ToString(),
                none => string.Empty
            );
        }
    }

    [DebuggerStepThrough]
    public static class Maybe
    {
        [Pure]
        public static Maybe<T> Some<T>([NotNull] T value) => Maybe<T>.Some(value);
        
        [Pure]
        public static None None() => default(None);
        
        [Pure]
        public static Maybe<T> None<T>() => Maybe<T>.None();

        [Pure]
        public static Maybe<T> OfNullable<T>([CanBeNull] T value) where T : class
        {
            return value == null
                ? Maybe<T>.None()
                : Maybe<T>.Some(value);
        }

        [Pure]
        public static Maybe<T> OfNullable<T>([CanBeNull] T? value) where T : struct
        {
            return value.HasValue ? Maybe<T>.Some(value.Value) : Maybe<T>.None();
        }

        public static AsyncMaybe<T> SomeAsync<T>([NotNull] T value) 
            => new AsyncMaybe<T>(Task.FromResult(Maybe<T>.Some(value)));
        
        public static AsyncMaybe<T> SomeAsync<T>([NotNull] Task<T> task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            
            return new AsyncMaybe<T>(task.Then(Maybe<T>.Some));
        }

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
        
        public static Maybe<T> Parse<T>(TypeParser<T> parser, [CanBeNull] string str)
        {
            return parser(str, out T result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<T> Parse<T>(NumberTypeParser<T> parser, [CanBeNull] string str, NumberStyles style, IFormatProvider provider)
        {
            return parser(str, style, provider, out T result)
                ? Some(result)
                : None();
        }
    }
}