using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class TryExtensions
    {
        public static Try<TResult> Map<T, TResult>(this Try<T> source, [InstantHandle, NotNull] Func<T, TResult> mapper)
        {
            return source.FlatMap(result => Try.Result(mapper(result)));
        }

        public static Try<TResult> FlatMap<T, TResult>(this Try<T> source, [InstantHandle, NotNull] Func<T, Try<TResult>> mapper)
        {
            return source.Match(mapper, Try.Exception<TResult>);
        }

        public static Try<T> Catch<T>(this Try<T> source, [InstantHandle, NotNull] Action<Exception> @catch)
        {
            return source.Match(
                Try.Result,
                exception =>
                {
                    @catch(exception);
                    return Try.Exception<T>(exception);
                }
            );
        }

        public static T OrReturn<T>(this Try<T> source, T value)
        {
            return source.Match(result => result, exception => value);
        }

        public static T OrGet<T>(this Try<T> source, [InstantHandle, NotNull] Func<T> value)
        {
            return source.Match(result => result, exception => value());
        }

        public static T OrThrow<T>(this Try<T> source)
        {
            if (source.IsException)
            {
                throw source.ExpectException();
            }
            return source.ExpectResult();
        }

        private static Exception ExpectException<T>(this Try<T> source)
        {
            return source.Exception()
                .OrThrow(
                    // Should never occur
                    () => new InvalidOperationException(
                        $"The value of the {nameof(Try<T>)} monad is in an invalid state. Expected an exception, but none was present."
                    )
                );
        }

        private static T ExpectResult<T>(this Try<T> source)
        {
            return source.Result()
                .OrThrow(
                    // Should never occur
                    () =>new InvalidOperationException(
                        $"The value of the {nameof(Try<T>)} monad is in an invalid state. Expected a result, but none was present."
                    )
                );
        }
    }
}
