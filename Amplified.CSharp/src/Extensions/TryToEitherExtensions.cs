using System;

namespace Amplified.CSharp.Extensions
{
    public static class TryToEitherExtensions
    {
        public static Either<Exception, T> ToEither<T>(this Try<T> source)
        {
            return source.Match(Either.Right<Exception, T>, Either.Left<Exception, T>);
        }
    }
}