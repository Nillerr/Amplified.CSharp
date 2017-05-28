using System;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeFilter
    {
        public static Maybe<T> Filter<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<T, bool> predicate)
        {
            return source.FlatMap(some => predicate(some) ? Some(some) : Maybe<T>.None());
        }
    }
}