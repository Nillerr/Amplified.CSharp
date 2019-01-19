using System.Threading.Tasks;
using Amplified.CSharp.Extensions.Continuations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class TaskToAsyncMaybe
    {
        public static AsyncMaybe<T> ToAsyncMaybe<T>(this Task<Maybe<T>> source)
        {
            return new AsyncMaybe<T>(source);
        }

        public static AsyncMaybe<T> ToAsyncMaybe<T>(this Task<AsyncMaybe<T>> source)
        {
            return source.Then(it => it.Match(some: Some, none: none => none)).ToAsyncMaybe();
        }
    }
}
