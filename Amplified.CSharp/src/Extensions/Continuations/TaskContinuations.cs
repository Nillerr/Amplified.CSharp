using System;
using System.Threading.Tasks;

namespace Amplified.CSharp.Extensions.Continuations
{
    public static class TaskContinuations
    {
        public static async Task<TResult> Then<T, TResult>(this Task<T> source, Func<T, Task<TResult>> async)
        {
            var result = await source.ConfigureAwait(false);
            return await async(result).ConfigureAwait(false);
        }
        
        public static async Task<TResult> Then<T, TResult>(this Task<T> source, Func<T, TResult> continuation)
        {
            var result = await source.ConfigureAwait(false);
            return continuation(result);
        }

        public static async Task<TResult> Then<TResult>(this Task source, Func<TResult> continuation)
        {
            await source.ConfigureAwait(false);
            return continuation();
        }

        public static async Task<TResult> WithResult<TResult>(this Task source, TResult result)
        {
            await source.ConfigureAwait(false);
            return result;
        }

        public static async Task<Unit> Then<T>(this Task<T> task, Action<T> continuation)
        {
            var result = await task.ConfigureAwait(false);
            continuation(result);
            return Unit.Instance;
        }
    }
}