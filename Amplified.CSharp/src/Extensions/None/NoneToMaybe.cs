using System;

namespace Amplified.CSharp.Extensions
{
    public static class NoneToMaybe
    {
        public static Maybe<T> ToMaybe<T>(this None none)
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
    }
}