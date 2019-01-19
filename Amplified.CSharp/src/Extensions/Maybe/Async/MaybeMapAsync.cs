using System;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions.Continuations;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeMapAsync
    {
        public static AsyncMaybe<TResult> MapAsync<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Task<TResult>> mapper)
        {
            return source.Match(
                some => mapper(some).Then(Maybe<TResult>.Some),
                none => Task.FromResult(Maybe<TResult>.None())
            ).ToAsyncMaybe();
        }
    }
}