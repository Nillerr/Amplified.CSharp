namespace Amplified.CSharp.Extensions
{
    public static class NullableToMaybeExtensions
    {
        public static Maybe<TSource> ToMaybe<TSource>(this TSource? source) where TSource : struct
        {
            return Maybe.OfNullable(source);
        }
    }
}