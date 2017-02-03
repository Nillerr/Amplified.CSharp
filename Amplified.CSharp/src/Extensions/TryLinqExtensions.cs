using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class TryLinqExtensions
    {
        public static Try<TResult> Select<T, TResult>(
            this Try<T> source,
            [InstantHandle, NotNull] Func<T, TResult> mapper
        )
        {
            return source.FlatMap(result => Try.Result(mapper(result)));
        }
    }
}