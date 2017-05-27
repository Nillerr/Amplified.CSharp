using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeLinqAsync
    {
        public static AsyncMaybe<TResult> SelectAsync<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Task<TResult>> mapper)
        {
            return source.MapAsync(mapper);
        }

        public static AsyncMaybe<T> WhereAsync<T>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Task<bool>> predicate)
        {
            return source.FilterAsync(predicate);
        }
    }
}