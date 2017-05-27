using System;
using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class NoneToAsyncMaybe
    {
        public static AsyncMaybe<T> ToAsyncMaybe<T>(this None none)
        {
            return AsyncMaybe<T>.None;
        }
        
        public static AsyncMaybe<TResult> ToSomeAsync<TResult>(this None none, Task<TResult> some)
        {
            return new AsyncMaybe<TResult>(some.Then(Some));
        }

        public static AsyncMaybe<TResult> ToSomeAsync<TResult>(this None none, Func<Task<TResult>> some)
        {
            return new AsyncMaybe<TResult>(some().Then(Some));
        }
    }
}