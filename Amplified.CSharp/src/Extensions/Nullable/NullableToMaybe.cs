namespace Amplified.CSharp.Extensions
{
    public static class NullableToMaybe
    {
        public static Maybe<TSource> ToMaybe<TSource>(this TSource? source) where TSource : struct
        {
            return Constructors.OfNullable(source);
        }
    }
}