using System;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeLinq
    {
        public static Maybe<TResult> Select<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, TResult> mapper
        )
        {
            return source.Match(some => Some(mapper(some)), none => Maybe<TResult>.None);
        }

        public static Maybe<T> Where<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<T, bool> predicate)
        {
            return source.FlatMap(some => predicate(some) ? Some(some) : Maybe<T>.None);
        }
    }
}