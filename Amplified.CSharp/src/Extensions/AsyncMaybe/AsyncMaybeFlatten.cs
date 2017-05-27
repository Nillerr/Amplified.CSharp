using System.Threading.Tasks;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeFlatten
    {
        public static AsyncMaybe<T> Flatten<T>(this AsyncMaybe<AsyncMaybe<T>> source)
        {
            var t = source.Match<AsyncMaybe<T>>(
                someAsync: async outerValue => await outerValue.Match<AsyncMaybe<T>>(
                    some: innerValue => new AsyncMaybe<T>(Task.FromResult<Maybe<T>>(Some<T>(innerValue))),
                    none: none => AsyncMaybe<T>.None
                ),
                none: none => AsyncMaybe<T>.None
            );

            return t.ToAsyncMaybe();
        }

        public static AsyncMaybe<T> Flatten<T>(this AsyncMaybe<Maybe<T>> source)
        {
            var t = source.Match<AsyncMaybe<T>>(
                some: outerValue => new AsyncMaybe<T>(Task.FromResult<Maybe<T>>(outerValue)),
                none: none => AsyncMaybe<T>.None
            );

            return t.ToAsyncMaybe();
        }
    }
}