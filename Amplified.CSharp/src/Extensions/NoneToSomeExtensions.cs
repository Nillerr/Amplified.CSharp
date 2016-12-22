using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class NoneToSomeExtensions
    {
        public static Some<TResult> ToSome<TResult>(this None source, [InstantHandle, NotNull] Func<TResult> selector)
        {
            return selector();
        }

        public static Some<TResult> ToSome<TResult>(this None source, TResult result)
        {
            return result;
        }
    }
}