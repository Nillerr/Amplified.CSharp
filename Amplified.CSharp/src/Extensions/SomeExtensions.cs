using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class SomeExtensions
    {
        public static Some<TResult> Map<T, TResult>(this Some<T> source, [InstantHandle, NotNull] Func<T, TResult> mapper)
        {
            return mapper(source);
        }

        public static Some<TResult> Map<T, TResult>(this Some<T> source, [InstantHandle, NotNull] Func<T, Some<TResult>> mapper)
        {
            return mapper(source);
        }
    }
}