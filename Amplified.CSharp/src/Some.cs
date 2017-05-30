using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Amplified.CSharp
{
    [DebuggerStepThrough]
    public struct Some<T> : IEquatable<Some<T>>, IEquatable<Maybe<T>>, IEquatable<None>
    {
        public Some(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public TResult Match<TResult>(Func<T, TResult> some)
        {
            return some(Value);
        }

        public static implicit operator T(Some<T> some)
        {
            return some.Value;
        }

        public static implicit operator Maybe<T>(Some<T> some)
        {
            return Maybe<T>.Some(some);
        }

        public bool Equals(Some<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public bool Equals(Maybe<T> other)
        {
            return other.Equals(this);
        }

        public bool Equals(None other)
        {
            return false;
        }
            
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return (obj is Some<T> && Equals((Some<T>) obj)) ||
                   (obj is Maybe<T> && Equals((Maybe<T>) obj));
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        public static bool operator ==(Some<T> lhs, Maybe<T> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Some<T> lhs, Maybe<T> rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static bool operator ==(Some<T> lhs, Some<T> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Some<T> lhs, Some<T> rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static bool operator ==(Some<T> lhs, None rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Some<T> lhs, None rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}