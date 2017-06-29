using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeLinqAsync
    {
        public static AsyncMaybe<TResult> SelectAsync<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<TResult>> mapper
        ) => source.MapAsync(mapper);

        public static AsyncMaybe<T> WhereAsync<T>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<bool>> predicate
        ) => source.FilterAsync(predicate);
    }
}
