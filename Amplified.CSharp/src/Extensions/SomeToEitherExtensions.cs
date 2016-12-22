namespace Amplified.CSharp.Extensions
{
    public static class SomeToEitherExtensions
    {
        public static Either<T, TRight> ToLeft<T, TRight>(this Some<T> source)
        {
            return Either.Left<T, TRight>(source);
        }

        public static Either<T, TRight> ToLeft<T, TRight>(this Some<T> source, Type<TRight> type)
        {
            return Either.Left<T, TRight>(source);
        }

        public static Either<TLeft, T> ToRight<TLeft, T>(this Some<T> source)
        {
            return Either.Right<TLeft, T>(source);
        }

        public static Either<TLeft, T> ToRight<TLeft, T>(this Some<T> source, Type<TLeft> type)
        {
            return Either.Right<TLeft, T>(source);
        }
    }
}