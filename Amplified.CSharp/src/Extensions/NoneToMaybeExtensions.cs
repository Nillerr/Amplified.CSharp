namespace Amplified.CSharp.Extensions
{
    public static class NoneToMaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this None none)
        {
            return Maybe<T>.None;
        }
    }
}