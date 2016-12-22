using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class TryToSomeExtensions
    {
        public static Some<T> OrSome<T>(this Try<T> source, T value)
        {
            return source.OrReturn(value);
        }

        public static Some<T> OrSome<T>(this Try<T> source, [InstantHandle, NotNull] Func<T> value)
        {
            return source.OrGet(value);
        }
    }
}