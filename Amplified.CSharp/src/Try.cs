using System;
using System.Collections.Generic;
using Amplified.CSharp.Extensions;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
    /// <summary>
    ///     Represents a potentially exceptional attempt to retrieve a result.
    /// </summary>
    /// <remarks>
    ///     The <c>Try</c> monad is a specialized version of the <c>Either</c> monad, where <c>TLeft</c> is predefined
    ///     as an <c>Exception</c>.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public struct Try<T> : IEquatable<Try<T>>, IEquatable<Either<Exception, T>>, IEquatable<Some<T>>, IEquatable<T>
    {
        private readonly T _result;
        private readonly Exception _exception;

        public Try(T result) : this()
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            _result = result;
            IsResult = true;
        }

        public Try(Exception exception) : this()
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            _exception = exception;
            IsException = true;
        }

        public bool IsResult { get; }
        public bool IsException { get; }

        public static implicit operator Try<T>(T result)
        {
            return new Try<T>(result);
        }

        public static implicit operator Try<T>(Some<T> result)
        {
            return new Try<T>(result);
        }

        public static implicit operator Try<T>(Exception exception)
        {
            return new Try<T>(exception);
        }

        public Some<TResult> MatchToSome<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> result,
            [InstantHandle, NotNull] Func<Exception, TResult> exception
        )
        {
            if (IsResult)
            {
                return result(_result);
            }

            if (IsException)
            {
                return exception(_exception);
            }

            throw new InvalidOperationException($"The {nameof(Try<TResult>)} was created without a value.");
        }

        public TResult Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> result,
            [InstantHandle, NotNull] Func<Exception, TResult> exception
        )
        {
            if (IsResult)
            {
                return result(_result);
            }

            if (IsException)
            {
                return exception(_exception);
            }

            throw new InvalidOperationException($"The {nameof(Try<TResult>)} was created without a value.");
        }

        public None Match(
            [InstantHandle, CanBeNull] Action<T> result = null,
            [InstantHandle, CanBeNull] Action<Exception> exception = null
        )
        {
            if (IsResult)
            {
                result?.Invoke(_result);
            }
            else if (IsException)
            {
                exception?.Invoke(_exception);
            }
            else
            {
                throw new InvalidOperationException($"The {nameof(Try<T>)} was created without a value.");
            }
            return None._;
        }

        public Try<T> Catch<TException>([InstantHandle, NotNull] Action<TException> @catch)
            where TException : Exception
        {
            return Match(
                Try.Result,
                exception =>
                {
                    var ex = exception as TException;
                    if (ex != null)
                    {
                        @catch(ex);
                    }
                    return Try.Exception<T>(exception);
                }
            );
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return (obj is Try<T> && Equals((Try<T>) obj)) ||
                   (obj is Either<Exception, T> && Equals((Either<Exception, T>) obj));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EqualityComparer<T>.Default.GetHashCode(_result);
                hashCode = (hashCode * 397) ^ (_exception?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ IsResult.GetHashCode();
                hashCode = (hashCode * 397) ^ IsException.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Try<T> left, Try<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Try<T> left, Try<T> right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(Try<T> left, Either<Exception, T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Try<T> left, Either<Exception, T> right)
        {
            return left.Equals(right) == false;
        }

        public static bool operator ==(Try<T> left, Some<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Try<T> left, Some<T> right)
        {
            return left.Equals(right) == false;
        }

        public static bool operator ==(Try<T> left, T right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Try<T> left, T right)
        {
            return left.Equals(right) == false;
        }

        public bool Equals(Try<T> other)
        {
            return IsResult == other.IsResult &&
                   IsException == other.IsException &&
                   EqualityComparer<T>.Default.Equals(_result, other._result) &&
                   Equals(_exception, other._exception);
        }

        public bool Equals(Either<Exception, T> other)
        {
            return IsResult == other.IsRight &&
                   IsException == other.IsLeft &&
                   Match(
                       result => other.Right()
                           .Filter(right => EqualityComparer<T>.Default.Equals(result, right))
                           .IsSome,
                       exception => other.Left()
                           .Filter(left => Equals(exception, left))
                           .IsSome
                   );
        }

        public bool Equals(Some<T> other)
        {
            return Equals(other.Value);
        }

        public bool Equals(T other)
        {
            return Match(result => EqualityComparer<T>.Default.Equals(result, other), _ => false);
        }

        public override string ToString()
        {
            return Match(
                result => $"Result({result})",
                exception => $"Exception({exception.GetType()})"
            );
        }
    }

    public static class Try
    {
        public static Try<T> Result<T>(T result)
        {
            return new Try<T>(result);
        }

        public static Try<T> Exception<T>(Exception exception)
        {
            return new Try<T>(exception);
        }

        public static Try<T> _<T>([InstantHandle, NotNull] Func<T> func)
        {
            try
            {
                return Result(func());
            }
            catch (Exception ex)
            {
                return Exception<T>(ex);
            }
        }
    }
}