using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeLinqExtensions
    {
        public static Maybe<TResult> Select<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, TResult> mapper
        )
        {
            return source.Match<Maybe<TResult>>(
                some => mapper(some), // Result is implicitly converted to Maybe
                none => none // None is implicitly converted to Maybe
            );
        }

        public static Maybe<T> Filter<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<T, bool> predicate)
        {
            return source.FlatMap(some => predicate(some) ? Maybe.Some(some) : Maybe<T>.None);
        }
    }
}