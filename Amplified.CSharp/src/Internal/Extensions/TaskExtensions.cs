using System;
using System.Threading.Tasks;

namespace Amplified.CSharp.Internal.Extensions
{
    internal static class TaskExtensions
    {
        public static async Task<TResult> Then<T, TResult>(this Task<T> source, Func<T, Task<TResult>> continuation)
        {
            var result = await source.ConfigureAwait(false);
            return await continuation(result).ConfigureAwait(false);
        }
        
        public static async Task<TResult> Then<T, TResult>(this Task<T> source, Func<T, TResult> continuation)
        {
            var result = await source.ConfigureAwait(false);
            return continuation(result);
        }
    }
}