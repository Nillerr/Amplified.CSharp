using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeZipFunc
    {
        public static Maybe<TResult> Zip<T1, T2, TResult>(
            this Maybe<T1> first,
            [InstantHandle, NotNull] Func<T1, Maybe<T2>> second,
            [InstantHandle, NotNull] Func<T1, T2, TResult> zipper
        )
        {
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            
            if (zipper == null)
                throw new ArgumentNullException(nameof(zipper));

            return first.FlatMap(
                some1 => second(some1).Map(
                    some2 => zipper(some1, some2)
                )
            );
        }
    }
}