using System;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

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
        
        public static AsyncMaybe<TResult> Map<TResult>(
            this AsyncMaybe<Unit> source,
            [NotNull] Func<TResult> mapper)
        {
            return source.Match(unit => AsyncMaybe<TResult>.Some(mapper()), none: AsyncMaybe<TResult>.None).ToAsyncMaybe();
        }

        public static AsyncMaybe<Unit> Map<T>(
            this AsyncMaybe<T> source,
            [NotNull] Action<T> mapper)
        {
            return source.Match(some => AsyncMaybe<Unit>.Some(mapper.WithUnitResult()(some)), none: AsyncMaybe<Unit>.None).ToAsyncMaybe();
        }
    }
}