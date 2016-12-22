using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeToTryExtensions
    {
        /// <summary>
        ///     Equivalent to <c>source.Map(some => Try._(() => func(some)));</c>
        /// </summary>
        public static Maybe<Try<TResult>> Try<T, TResult>(this Maybe<T> source, [InstantHandle, NotNull] Func<T, TResult> func)
        {
            return source.Map(some => CSharp.Try._(() => func(some)));
        }

        public static Maybe<Try<T>> Catch<T>(this Maybe<Try<T>> source, [InstantHandle, NotNull] Action<Exception> handler)
        {
            return source.Map(some => some.Catch(handler));
        }

        public static Try<T> ToTry<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<Exception> exception)
        {
            return source.Match(
                CSharp.Try.Result,
                none => CSharp.Try.Exception<T>(exception())
            );
        }

        public static Try<T> ToTry<T>(this Maybe<T> source, Exception exception)
        {
            return source.Match(
                CSharp.Try.Result,
                none => CSharp.Try.Exception<T>(exception)
            );
        }
    }
}