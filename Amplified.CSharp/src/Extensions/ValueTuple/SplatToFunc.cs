using System;

namespace Amplified.CSharp.Extensions.ValueTuple
{
    public static class SplatToFunc
    {
        public static TResult Splat<T1, T2, TResult>(this (T1, T2) source, Func<T1, T2, TResult> action)
        {
            return action(source.Item1, source.Item2);
        }
        
        public static TResult Splat<T1, T2, T3, TResult>(this (T1, T2, T3) source, Func<T1, T2, T3, TResult> action)
        {
            return action(source.Item1, source.Item2, source.Item3);
        }
        
        public static TResult Splat<T1, T2, T3, T4, TResult>(this (T1, T2, T3, T4) source, Func<T1, T2, T3, T4, TResult> action)
        {
            return action(source.Item1, source.Item2, source.Item3, source.Item4);
        }
        
        public static TResult Splat<T1, T2, T3, T4, T5, TResult>(this (T1, T2, T3, T4, T5) source, Func<T1, T2, T3, T4, T5, TResult> action)
        {
            return action(source.Item1, source.Item2, source.Item3, source.Item4, source.Item5);
        }
    }
}