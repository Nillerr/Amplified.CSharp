using System;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions.Continuations;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeFilterAsync
    {
        public static AsyncMaybe<T> FilterAsync<T>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Task<bool>> predicate)
        {
            return source.Match(
                some => predicate(some).Then(matches => matches ? Maybe<T>.Some(some) : Maybe<T>.None()),
                none => Task.FromResult(Maybe<T>.None())
            ).ToAsyncMaybe();
        }
    }
}