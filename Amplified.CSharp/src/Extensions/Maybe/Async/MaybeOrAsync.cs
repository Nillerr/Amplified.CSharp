using System;
using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeOrAsync
    {
        public static Task<T> OrReturnAsync<T>(this Maybe<T> source, Task<T> other)
        {
            return source.Match(
                Task.FromResult,
                none => other
            );
        }

        public static Task<T> OrGetAsync<T>(this Maybe<T> source, Func<Task<T>> other)
        {
            return source.Match(Task.FromResult, none => other());
        }

        public static async Task<T> OrThrowAsync<T>(this Maybe<T> source, Func<Task<Exception>> other)
        {
            return await source.Match<Task<T>>(
                Task.FromResult, 
                none => other().Then<Exception, T>(continuation: ex => throw ex)
            );
        }

        public static AsyncMaybe<T> OrAsync<T>(this Maybe<T> source, AsyncMaybe<T> other)
        {
            return source.Match(
                some => new AsyncMaybe<T>(Task.FromResult(source)),
                none => other
            );
        }

        public static AsyncMaybe<T> OrAsync<T>(this Maybe<T> source, Func<AsyncMaybe<T>> other)
        {
            return source.Match(
                some => new AsyncMaybe<T>(Task.FromResult(source)),
                none => other()
            );
        }
    }
}
