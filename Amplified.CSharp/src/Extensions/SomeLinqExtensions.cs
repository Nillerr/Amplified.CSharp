using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class SomeLinqExtensions
    {
        public static Some<TResult> Select<T, TResult>(this Some<T> source, [InstantHandle, NotNull] Func<T, TResult> mapper)
        {
            return mapper(source);
        }

        public static Some<TResult> Select<T, TResult>(this Some<T> source, [InstantHandle, NotNull] Func<T, Some<TResult>> mapper)
        {
            return mapper(source);
        }
    }
}