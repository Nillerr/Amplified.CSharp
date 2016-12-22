namespace Amplified.CSharp.Extensions
{
    public static class EitherToMaybeExtensions
    {
        public static Maybe<TLeft> Left<TLeft, TRight>(this Either<TLeft, TRight> source)
        {
            return source.Match(Maybe.Some, right => Maybe<TLeft>.None);
        }

        public static Maybe<TRight> Right<TLeft, TRight>(this Either<TLeft, TRight> source)
        {
            return source.Match(left => Maybe<TRight>.None, Maybe.Some);
        }
    }
}