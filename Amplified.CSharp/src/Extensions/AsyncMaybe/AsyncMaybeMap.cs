using System;
using JetBrains.Annotations;
using static Amplified.CSharp.Constructors;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeMap
    {
        public static AsyncMaybe<TResult> Map<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, TResult> mapper)
        {
            return source.Match(some => Some(mapper(some)), none => Maybe<TResult>.None).ToAsyncMaybe();
        }
        
        public static AsyncMaybe<TResult> Map<TResult>(
            this AsyncMaybe<Unit> source,
            [NotNull] Func<TResult> mapper)
        {
            return source.Match(unit => Some(mapper()), none => Maybe<TResult>.None).ToAsyncMaybe();
        }

        public static AsyncMaybe<Unit> Map<T>(
            this AsyncMaybe<T> source,
            [NotNull] Action<T> mapper)
        {
            return source.Match(some => Some(mapper.ToUnit()(some)), none => Maybe<Unit>.None).ToAsyncMaybe();
        }
    }
}