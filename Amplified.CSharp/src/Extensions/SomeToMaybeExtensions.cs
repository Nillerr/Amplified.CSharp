using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class SomeToMaybeExtensions
    {
        public static Maybe<T> Filter<T>(this Some<T> source, [InstantHandle, NotNull] Func<bool> predicate)
        {
            return predicate() ? source : Maybe<T>.None;
        }

        public static Maybe<T> ToMaybe<T>(this Some<T> source)
        {
            return Maybe.Some(source);
        }
    }
}