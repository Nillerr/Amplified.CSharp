using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeMap
    {
        public static AsyncMaybe<TResult> Map<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, TResult> mapper)
        {
            return source.Match(some => AsyncMaybe<TResult>.Some(mapper(some)), none: AsyncMaybe<TResult>.None).ToAsyncMaybe();
        }
    }
}