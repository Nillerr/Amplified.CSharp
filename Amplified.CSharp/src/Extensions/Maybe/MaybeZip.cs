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
            
            return first.FlatMap<T1, TResult>(
                some1 => second.Map<T2, TResult>(
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
            
            return first.FlatMap<T1, TResult>(
                some1 => second.FlatMap<T2, TResult>(
                    some2 => third.Map<T3, TResult>(
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
            
            return first.FlatMap<T1, TResult>(
                some1 => second.FlatMap<T2, TResult>(
                    some2 => third.FlatMap<T3, TResult>(
                        some3 => fourth.Map<T4, TResult>(
                            some4 => zipper(some1, some2, some3, some4)
                        )
                    )
                )
            );
        }
    }
}