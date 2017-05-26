using System.Threading.Tasks;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeToAsyncMaybeExtensions
    {
        public static AsyncMaybe<T> ToAsync<T>(this Maybe<T> source)
        {
            return new AsyncMaybe<T>(Task.FromResult(source));
        }
    }
}
