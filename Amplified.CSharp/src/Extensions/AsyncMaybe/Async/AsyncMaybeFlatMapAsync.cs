using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeFlatMapAsync
    {
        public static AsyncMaybe<TResult> FlatMapAsync<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<Maybe<TResult>>> mapper)
        {
            return source.Match(mapper, none => Maybe<TResult>.None()).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> FlatMapAsync<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<AsyncMaybe<TResult>>> mapper)
        {
            return source.Match(mapper, none => AsyncMaybe<TResult>.None()).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> FlatMapUnitAsync<TResult>(
            this AsyncMaybe<Unit> source,
            [NotNull] Func<Task<Maybe<TResult>>> mapper)
        {
            return source.Match(unit => mapper(), none: Maybe<TResult>.None).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> FlatMapUnitAsync<TResult>(
            this AsyncMaybe<Unit> source,
            [NotNull] Func<Task<AsyncMaybe<TResult>>> mapper)
        {
            return source.Match(unit => mapper(), none: AsyncMaybe<TResult>.None).ToAsyncMaybe();
        }
    }
}