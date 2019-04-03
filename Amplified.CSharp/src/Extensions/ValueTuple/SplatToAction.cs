using System;

namespace Amplified.CSharp.Extensions.ValueTuple
{
    public static class SplatToAction
    {
        public static void Splat<T1, T2>(this (T1, T2) source, Action<T1, T2> action)
        {
            action(source.Item1, source.Item2);
        }
        
        public static void Splat<T1, T2, T3>(this (T1, T2, T3) source, Action<T1, T2, T3> action)
        {
            action(source.Item1, source.Item2, source.Item3);
        }
        
        public static void Splat<T1, T2, T3, T4>(this (T1, T2, T3, T4) source, Action<T1, T2, T3, T4> action)
        {
            action(source.Item1, source.Item2, source.Item3, source.Item4);
        }
        
        public static void Splat<T1, T2, T3, T4, T5>(this (T1, T2, T3, T4, T5) source, Action<T1, T2, T3, T4, T5> action)
        {
            action(source.Item1, source.Item2, source.Item3, source.Item4, source.Item5);
        }
    }
}