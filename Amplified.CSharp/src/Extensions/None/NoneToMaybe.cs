using System;

namespace Amplified.CSharp.Extensions
{
    public static class NoneToMaybe
    {
        public static Maybe<T> ToMaybe<T>(this None none)
        {
            return Maybe<T>.None();
        }

        public static Maybe<TResult> ToSome<TResult>(this None none, TResult some)
        {
            return Maybe<TResult>.Some(some);
        }

        public static Maybe<TResult> ToSome<TResult>(this None none, Func<TResult> some)
        {
            return Maybe<TResult>.Some(some());
        }
    }
}