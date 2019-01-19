using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeZipFuncValueTuple
    {
        public static Maybe<(T1, T2)> Zip<T1, T2>(
            this Maybe<T1> source,
            [InstantHandle, NotNull] Func<T1, Maybe<T2>> other
        )
        {
            return source.Zip(other, (s, o) => (s, o));
        }
        
        public static Maybe<(T1, T2, T3)> Zip<T1, T2, T3>(
            this Maybe<(T1, T2)> source,
            [InstantHandle, NotNull] Func<T1, T2, Maybe<T3>> other
        )
        {
            return source.Zip(
                s => other(s.Item1, s.Item2), 
                (s, o)=> (s.Item1, s.Item2, o)
            );
        }
        
        public static Maybe<(T1, T2, T3, T4)> Zip<T1, T2, T3, T4>(
            this Maybe<(T1, T2, T3)> source,
            [InstantHandle, NotNull] Func<T1, T2, T3, Maybe<T4>> other
        )
        {
            return source.Zip(
                s => other(s.Item1, s.Item2, s.Item3), 
                (s, o)=> (s.Item1, s.Item2, s.Item3, o)
            );
        }
        
        public static Maybe<(T1, T2, T3, T4, T5)> Zip<T1, T2, T3, T4, T5>(
            this Maybe<(T1, T2, T3, T4)> source,
            [InstantHandle, NotNull] Func<T1, T2, T3, T4, Maybe<T5>> other
        )
        {
            return source.Zip(
                s => other(s.Item1, s.Item2, s.Item3, s.Item4), 
                (s, o)=> (s.Item1, s.Item2, s.Item3, s.Item4, o)
            );
        }
    }
}