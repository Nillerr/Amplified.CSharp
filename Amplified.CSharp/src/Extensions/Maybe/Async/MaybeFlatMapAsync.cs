using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeFlatMapAsync
    {
        public static AsyncMaybe<TResult> FlatMapAsync<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Task<Maybe<TResult>>> mapper)
        {
            return source.Match(
                mapper,
                none => Task.FromResult(Maybe<TResult>.None)
            ).ToAsyncMaybe();
        }
    }
}