using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
    /// <summary>
    ///     Represents the presence of a value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Some<T> : IEquatable<Some<T>>, IEquatable<Maybe<T>>, IEquatable<T>, ICanBeNone
    {
        public Some(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), $"A {nameof(Some<T>)} cannot be created with a null value.");
            }

            Value = value;
        }

        public T Value { get; }

        public bool IsSome => true;
        public bool IsNone => false;

        public static implicit operator Some<T>(T value)
        {
            return new Some<T>(value);
        }

        public static implicit operator T(Some<T> some)
        {
            return some.Value;
        }

        public Some<TResult> MatchToSome<TResult>([InstantHandle, NotNull] Func<T, TResult> some)
        {
            return some(Value);
        }

        public TResult Match<TResult>([InstantHandle, NotNull] Func<T, TResult> some)
        {
            return some(Value);
        }

        public None Match([InstantHandle, NotNull] Action<T> some)
        {
            some(Value);
            return None._;
        }

        public bool Equals(Some<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public bool Equals(T other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }

        public bool Equals(Maybe<T> other)
        {
            return other.Equals(this);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return (obj is Some<T> && Equals((Some<T>) obj)) ||
                   (obj is Maybe<T> && Equals((Maybe<T>) obj)) ||
                   (obj is T && Equals((T) obj));
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public static bool operator ==(Some<T> left, Some<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Some<T> left, Some<T> right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(Some<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Some<T> left, Maybe<T> right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(Some<T> left, T right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Some<T> left, T right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"Some({Value})";
        }
    }

    public static class Some
    {
        public static Some<T> Of<T>([NotNull] T value)
        {
            return value;
        }

        public static Some<T> _<T>([NotNull] T value)
        {
            return value;
        }

        public static Some<T> Of<T>([NotNull] Func<T> value)
        {
            return value();
        }
    }
}