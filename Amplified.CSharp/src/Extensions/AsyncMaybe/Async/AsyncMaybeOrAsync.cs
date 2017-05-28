using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeOrAsync
    {
        public static Task<T> OrReturnAsync<T>(this AsyncMaybe<T> source, Task<T> value)
        {
            return source.Match(
                some => some,
                none => value
            );
        }

        public static Task<T> OrGetAsync<T>(this AsyncMaybe<T> source, [InstantHandle, NotNull] Func<Task<T>> value)
        {
            return source.Match(
                some => some,
                none => value()
            );
        }

        public static async Task<T> OrThrowAsync<T>(this AsyncMaybe<T> source, [InstantHandle, NotNull] Func<Task<Exception>> exception)
        {
            return await source.Match<T>(
                some: some => some,
                noneAsync: async _ => throw await exception() 
            );
        }

        public static async Task<T> OrThrowAsync<T>(this AsyncMaybe<T> source, [NotNull] Task<Exception> exception)
        {
            return await source.Match<T>(
                some: some => some,
                noneAsync: async _ => throw await exception
            );
        }

        public static AsyncMaybe<T> OrAsync<T>(this AsyncMaybe<T> source, [InstantHandle, NotNull] Func<Task<AsyncMaybe<T>>> other)
        {
            return source.Match(
                some: some => Some(some).ToAsync(),
                noneAsync: none => other()
            ).ToAsyncMaybe();
        }

        public static AsyncMaybe<T> OrAsync<T>(this AsyncMaybe<T> source, [InstantHandle, NotNull] Func<Task<Maybe<T>>> other)
        {
            return source.Match(
                some: Some,
                noneAsync: none => other()
            ).ToAsyncMaybe();
        }
    }
}