using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeZipFuncValueTuple
    {
        public static AsyncMaybe<(T1, T2)> Zip<T1, T2>(
            this AsyncMaybe<T1> first,
            [NotNull] Func<T1, AsyncMaybe<T2>> second
        )
        {
            return first.Zip(second, (f, s) => (f, s));
        }
        
        public static AsyncMaybe<(T1, T2, T3)> Zip<T1, T2, T3>(
            this AsyncMaybe<(T1, T2)> first,
            [NotNull] Func<T1, T2, AsyncMaybe<T3>> second
        )
        {
            return first.Zip(
                f => second(f.Item1, f.Item2),
                (f, s) => (f.Item1, f.Item2, s)
            );
        }
        
        public static AsyncMaybe<(T1, T2, T3, T4)> Zip<T1, T2, T3, T4>(
            this AsyncMaybe<(T1, T2, T3)> first,
            [NotNull] Func<T1, T2, T3, AsyncMaybe<T4>> second
        )
        {
            return first.Zip(
                f => second(f.Item1, f.Item2, f.Item3),
                (f, s) => (f.Item1, f.Item2, f.Item3, s)
            );
        }
        
        public static AsyncMaybe<(T1, T2, T3, T4, T5)> Zip<T1, T2, T3, T4, T5>(
            this AsyncMaybe<(T1, T2, T3, T4)> first,
            [NotNull] Func<T1, T2, T3, T4, AsyncMaybe<T5>> second
        )
        {
            return first.Zip(
                f => second(f.Item1, f.Item2, f.Item3, f.Item4),
                (f, s) => (f.Item1, f.Item2, f.Item3, f.Item4, s)
            );
        }
    }
}