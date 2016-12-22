using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class SomeToTryExtensions
    {
        public static Try<TResult> Try<T, TResult>(this Some<T> source, [InstantHandle, NotNull] Func<T, TResult> @try)
        {
            return CSharp.Try._(() => @try(source));
        }
    }
}