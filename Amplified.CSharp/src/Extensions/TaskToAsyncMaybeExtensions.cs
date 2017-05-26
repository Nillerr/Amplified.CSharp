using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class TaskToAsyncMaybeExtensions
    {
        public static AsyncMaybe<T> ToAsync<T>(this Task<Maybe<T>> source)
        {
            return new AsyncMaybe<T>(source);
        }

        public static AsyncMaybe<T> ToAsync<T>(this Task<AsyncMaybe<T>> source)
        {
            return new AsyncMaybe<T>(source.Then(it => it.Match(some: Some, none: none => none)));
        }
    }
}
