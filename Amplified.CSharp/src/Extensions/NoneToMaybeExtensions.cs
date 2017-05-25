using System;
using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class NoneToMaybeExtensions
    {
        public static Maybe<T> AsMaybe<T>(this None none)
        {
            return Maybe<T>.None;
        }

        public static Maybe<TResult> ToSome<TResult>(this None none, TResult some)
        {
            return new Maybe<TResult>(some);
        }

        public static Maybe<TResult> ToSome<TResult>(this None none, Func<TResult> some)
        {
            return new Maybe<TResult>(some());
        }

        public static AsyncMaybe<TResult> ToSome<TResult>(this None none, Task<TResult> some)
        {
            return new AsyncMaybe<TResult>(some.Then(Some));
        }

        public static AsyncMaybe<TResult> ToSome<TResult>(this None none, Func<Task<TResult>> some)
        {
            return new AsyncMaybe<TResult>(some().Then(Some));
        }
    }
}