using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeZip
    {
        public static Maybe<TResult> Zip<T1, T2, TResult>(
            this Maybe<T1> first,
            Maybe<T2> second,
            [InstantHandle, NotNull] Func<T1, T2, TResult> zipper
        )
        {
            if (zipper == null)
                throw new ArgumentNullException(nameof(zipper));
            
            return first.FlatMap(
                some1 => second.Map(
                    some2 => zipper(some1, some2)
                )
            );
        }

        public static Maybe<TResult> Zip<T1, T2, T3, TResult>(
            this Maybe<T1> first,
            Maybe<T2> second,
            Maybe<T3> third,
            [InstantHandle, NotNull] Func<T1, T2, T3, TResult> zipper
        )
        {
            if (zipper == null)
                throw new ArgumentNullException(nameof(zipper));
            
            return first.FlatMap(
                some1 => second.FlatMap(
                    some2 => third.Map(
                        some3 => zipper(some1, some2, some3)
                    )
                )
            );
        }

        public static Maybe<TResult> Zip<T1, T2, T3, T4, TResult>(
            this Maybe<T1> first,
            Maybe<T2> second,
            Maybe<T3> third,
            Maybe<T4> fourth,
            [InstantHandle, NotNull] Func<T1, T2, T3, T4, TResult> zipper
        )
        {
            if (zipper == null)
                throw new ArgumentNullException(nameof(zipper));
            
            return first.FlatMap(
                some1 => second.FlatMap(
                    some2 => third.FlatMap(
                        some3 => fourth.Map(
                            some4 => zipper(some1, some2, some3, some4)
                        )
                    )
                )
            );
        }
    }
}