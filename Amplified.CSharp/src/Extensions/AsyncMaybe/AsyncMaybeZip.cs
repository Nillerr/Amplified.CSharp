using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeZip
    {
        public static AsyncMaybe<TResult> Zip<T1, T2, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            [NotNull] Func<T1, T2, TResult> zipper
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

        public static AsyncMaybe<TResult> Zip<T1, T2, T3, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            AsyncMaybe<T3> third,
            [NotNull] Func<T1, T2, T3, TResult> zipper
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

        public static AsyncMaybe<TResult> Zip<T1, T2, T3, T4, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            AsyncMaybe<T3> third,
            AsyncMaybe<T4> fourth,
            [NotNull] Func<T1, T2, T3, T4, TResult> zipper
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