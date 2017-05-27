using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeMap
    {
        public static Maybe<TResult> Map<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, TResult> mapper)
        {
            return source.Match(
                some => Maybe.Some(mapper(some)),
                none => Maybe<TResult>.None
            );
        }
    }
}