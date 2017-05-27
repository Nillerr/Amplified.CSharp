using System;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeFilter
    {
        public static AsyncMaybe<T> Filter<T>(
            this AsyncMaybe<T> source,
            [InstantHandle, NotNull] Func<T, bool> predicate)
        {
            return source.Match(
                some => predicate(some)
                    ? Some(some)
                    : Maybe<T>.None,
                none => Maybe<T>.None
            ).ToAsyncMaybe();
        }
    }
}