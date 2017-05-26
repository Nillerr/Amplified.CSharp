using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeLinqExtensions
    {
        public static AsyncMaybe<TResult> Select<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, TResult> mapper
        ) => source.Map(mapper);
        
        public static AsyncMaybe<TResult> Select<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<TResult>> mapper
        ) => source.Map(mapper);
        
        public static AsyncMaybe<T> Where<T>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, bool> predicate
        ) => source.Filter(predicate);
        
        public static AsyncMaybe<T> Where<T>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<bool>> predicate
        ) => source.Filter(predicate);
    }
}
