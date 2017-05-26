namespace Amplified.CSharp.Extensions
{
    public static class NoneToAsyncMaybeExtensions
    {
        public static AsyncMaybe<T> ToAsync<T>(this None none)
        {
            return AsyncMaybe<T>.None;
        }
    }
}
