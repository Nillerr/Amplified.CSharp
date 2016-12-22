namespace Amplified.CSharp.Extensions
{
    public static class MaybeToNoneExtensions
    {
        public static None ToNone<T>(this Maybe<T> source)
        {
            return None._;
        }
    }
}