using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeZipFunc
    {
        public static AsyncMaybe<TResult> Zip<T1, T2, TResult>(
            this AsyncMaybe<T1> first,
            [NotNull] Func<T1, AsyncMaybe<T2>> second,
            [NotNull] Func<T1, T2, TResult> zipper
        )
        {
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