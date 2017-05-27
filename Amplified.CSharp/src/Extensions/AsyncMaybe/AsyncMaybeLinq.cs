using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeLinq
    {
        public static AsyncMaybe<TResult> Select<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, TResult> mapper
        ) => source.Map(mapper);
        
        public static AsyncMaybe<Unit> Select<T>(
            this AsyncMaybe<T> source,
            [NotNull] Action<T> mapper
        ) => source.Map(mapper);

        public static AsyncMaybe<T> Where<T>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, bool> predicate
        ) => source.Filter(predicate);
    }
}