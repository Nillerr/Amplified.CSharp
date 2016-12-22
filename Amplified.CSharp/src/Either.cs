using System;
using System.Collections.Generic;
using Amplified.CSharp.Extensions;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
    /// <summary>
    ///     Represents the possibility of a value being one, and only one, of two potential outcomes.
    /// </summary>
    /// <remarks>
    ///     While <typeparamref name="TLeft"/> by convention is used for errors, such as an <c>enum</c>, and
    ///     <typeparamref name="TRight"/> is used for a result, there are no restrictions on what type of values you
    ///     use with this type.
    /// </remarks>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    public struct Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>, IEquatable<Try<TRight>>
    {
        private readonly Some<TLeft> _left;
        private readonly Some<TRight> _right;

        public Either(Some<TLeft> left) : this()
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            _left = left;
            IsLeft = true;
        }

        public Either(Some<TRight> right) : this()
        {
            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            _right = right;
            IsRight = true;
        }

        public bool IsLeft { get; }
        public bool IsRight { get; }

        public Some<TResult> MatchToSome<TResult>(
            [InstantHandle, NotNull] Func<TLeft, TResult> left,
            [InstantHandle, NotNull] Func<TRight, TResult> right
        )
        {
            if (IsLeft)
            {
                return left(_left);
            }
            if (IsRight)
            {
                return right(_right);
            }
            throw new InvalidOperationException();
        }

        public TResult Match<TResult>(
            [InstantHandle, NotNull] Func<TLeft, TResult> left,
            [InstantHandle, NotNull] Func<TRight, TResult> right
        )
        {
            if (IsLeft)
            {
                return left(_left);
            }
            if (IsRight)
            {
                return right(_right);
            }
            throw new InvalidOperationException();
        }

        public None Match(
            [InstantHandle, CanBeNull] Action<TLeft> left = null,
            [InstantHandle, CanBeNull] Action<TRight> right = null
        )
        {
            if (IsLeft)
            {
                left?.Invoke(_left);
            }
            else if (IsRight)
            {
                right?.Invoke(_right);
            }
            else
            {
                throw new InvalidOperationException();
            }
            return None._;
        }

        public bool Equals(Either<TLeft, TRight> other)
        {
            return IsLeft == other.IsLeft && IsRight == other.IsRight &&
                   EqualityComparer<TLeft>.Default.Equals(_left, other._left) &&
                   EqualityComparer<TRight>.Default.Equals(_right, other._right);
        }

        public bool Equals(Try<TRight> other)
        {
            return IsRight == other.IsResult &&
                   IsLeft == other.IsException &&
                   Match(
                       left => other.Exception()
                           .Filter(exception => Equals(exception, left))
                           .IsSome,
                       right => other.Result()
                           .Filter(result => EqualityComparer<TRight>.Default.Equals(result, right))
                           .IsSome
                   );
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Either<TLeft, TRight> && Equals((Either<TLeft, TRight>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EqualityComparer<TLeft>.Default.GetHashCode(_left);
                hashCode = (hashCode * 397) ^ EqualityComparer<TRight>.Default.GetHashCode(_right);
                hashCode = (hashCode * 397) ^ IsLeft.GetHashCode();
                hashCode = (hashCode * 397) ^ IsRight.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
        {
            return left.Equals(right) == false;
        }

        public static bool operator ==(Either<TLeft, TRight> left, Try<TRight> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Either<TLeft, TRight> left, Try<TRight> right)
        {
            return left.Equals(right) == false;
        }

        public override string ToString()
        {
            return Match(left => $"Left({left})", right => $"Right({right})");
        }
    }

    public static class Either
    {
        public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft left)
        {
            return new Either<TLeft, TRight>(left);
        }

        public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft left, Type<TRight> right)
        {
            return new Either<TLeft, TRight>(left);
        }

        public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight right)
        {
            return new Either<TLeft, TRight>(right);
        }

        public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight right, Type<TRight> type)
        {
            return new Either<TLeft, TRight>(right);
        }
    }
}