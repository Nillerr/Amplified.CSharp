using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeFlatMap
    {
        public static AsyncMaybe<TResult> FlatMap<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Maybe<TResult>> mapper)
        {
            return source.Match(mapper, none: Maybe<TResult>.None).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> FlatMap<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, AsyncMaybe<TResult>> mapper)
        {
            return source.Match(mapper, none: AsyncMaybe<TResult>.None).ToAsyncMaybe();
        }
    }
}