using System;

namespace Amplified.CSharp.Extensions
{
    public static class TryToMaybeExtensions
    {
        public static Maybe<T> Result<T>(this Try<T> source)
        {
            return source.Match(Maybe.Some, exception => Maybe<T>.None);
        }

        public static Maybe<Exception> Exception<T>(this Try<T> source)
        {
            return source.Match(result => Maybe<Exception>.None, Maybe.Some);
        }
    }
}