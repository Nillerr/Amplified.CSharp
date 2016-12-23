using System;
using System.Collections.Generic;
using Amplified.CSharp.Extensions;
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
        public static readonly Maybe<T> None = default(Maybe<T>);

        private readonly T _value;

        public Maybe(T value) : this()
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _value = value;
            IsSome = true;
        }

        public bool IsSome { get; }
        public bool IsNone => IsSome == false;

        public static implicit operator Maybe<T>(T some)
        {
            return new Maybe<T>(some);
        }

        public static implicit operator Maybe<T>(Some<T> some)
        {
            return new Maybe<T>(some);
        }

        public static implicit operator Maybe<T>(None none)
        {
            return None;
        }

        public Some<TResult> MatchToSome<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<None, TResult> none
        )
        {
            return IsSome ? some(_value) : none(CSharp.None.Instance);
        }

        public TResult Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<None, TResult> none
        )
        {
            return IsSome ? some(_value) : none(CSharp.None.Instance);
        }

        public None Match(
            [InstantHandle, CanBeNull] Action<T> some = null,
            [InstantHandle, CanBeNull] Action none = null
        )
        {
            if (IsSome)
            {
                some?.Invoke(_value);
            }
            else
            {
                none?.Invoke();
            }
            return CSharp.None.Instance;
        }

        public bool Equals(Maybe<T> other)
        {
            return (IsNone && other.IsNone) ||
                   (IsSome && IsSome && EqualityComparer<T>.Default.Equals(_value, other._value));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return (obj is Maybe<T> && Equals((Maybe<T>) obj)) ||
                   (obj is Some<T> && Equals((Some<T>) obj)) ||
                   (obj is None && Equals((None) obj));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ IsSome.GetHashCode();
            }
        }

        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(Maybe<T> left, Some<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Maybe<T> left, Some<T> right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(Maybe<T> left, None right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Maybe<T> left, None right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return Match(
                some => $"Some({some})",
                none => "None"
            );
        }

        public bool Equals(Some<T> other)
        {
            return Equals(other.ToMaybe());
        }

        public bool Equals(None other)
        {
            return Equals(other.ToMaybe<T>());
        }
    }

    public static class Maybe
    {
        public static Maybe<T> Some<T>(Some<T> value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<T> Some<T>([NotNull] T value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<T> None<T>()
        {
            return Maybe<T>.None;
        }

        public static Maybe<T> OfNullable<T>([CanBeNull] T value)
        {
            return value == null ? Maybe<T>.None : new Maybe<T>(value);
        }
    }
}