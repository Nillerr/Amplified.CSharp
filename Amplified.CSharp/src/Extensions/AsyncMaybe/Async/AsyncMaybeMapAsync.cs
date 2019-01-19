using System;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions.Continuations;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeMapAsync
    {
        public static AsyncMaybe<TResult> MapAsync<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<TResult>> mapperAsync)
        {
            return source.Match(some => mapperAsync(some).Then(Some), none => Maybe<TResult>.None()).ToAsyncMaybe();
        }
        
        public static AsyncMaybe<Unit> MapAsync<T>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task> mapperAsync)
        {
            return source.Match(some => mapperAsync(some).Then(() => Some(default(Unit))), none => Maybe<Unit>.None()).ToAsyncMaybe();
        }

        public static AsyncMaybe<Unit> MapAsync<T>(
            this AsyncMaybe<T> source,
            [NotNull] Func<Task> mapperAsync)
        {
            return source.Match(some => mapperAsync().Then(() => Some(default(Unit))), none => Maybe<Unit>.None()).ToAsyncMaybe();
        }
    }
}