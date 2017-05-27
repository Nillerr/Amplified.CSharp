using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeZipAsync
    {

        public static AsyncMaybe<TResult> ZipAsync<T1, T2, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            [InstantHandle, NotNull] Func<T1, T2, Task<TResult>> zipper
        )
        {
            return first.FlatMap(
                some1 => second.MapAsync(
                    some2 => zipper(some1, some2)
                )
            );
        }

        public static AsyncMaybe<TResult> ZipAsync<T1, T2, T3, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            AsyncMaybe<T3> third,
            [InstantHandle, NotNull] Func<T1, T2, T3, Task<TResult>> zipper
        )
        {
            return first.FlatMap<T1, TResult>(
                some1 => second.FlatMap<T2, TResult>(
                    some2 => third.MapAsync<T3, TResult>(
                        some3 => zipper(some1, some2, some3)
                    )
                )
            );
        }

        public static AsyncMaybe<TResult> ZipAsync<T1, T2, T3, T4, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            AsyncMaybe<T3> third,
            AsyncMaybe<T4> fourth,
            [InstantHandle, NotNull] Func<T1, T2, T3, T4, Task<TResult>> zipper
        )
        {
            return first.FlatMap<T1, TResult>(
                some1 => second.FlatMap<T2, TResult>(
                    some2 => third.FlatMap<T3, TResult>(
                        some3 => fourth.MapAsync<T4, TResult>(
                            some4 => zipper(some1, some2, some3, some4)
                        )
                    )
                )
            );
        }
    }
}