using System;
using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeFilterAsync
    {
        public static AsyncMaybe<T> FilterAsync<T>(
            this AsyncMaybe<T> source,
            [InstantHandle, NotNull] Func<T, Task<bool>> predicate)
        {
            return source.Match(
                some => predicate(some).Then(
                    it => it
                        ? Some(some)
                        : Maybe<T>.None),
                none => Maybe<T>.None
            ).ToAsyncMaybe();
        }
    }
}