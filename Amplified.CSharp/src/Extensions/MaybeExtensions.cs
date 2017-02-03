using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeExtensions
    {
        public static Maybe<TResult> Map<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, TResult> mapper)
        {
            return source.Match<Maybe<TResult>>(
                some => mapper(some), // Result is implicitly converted to Maybe
                none => none // None is implicitly converter to Maybe
            );
        }

        public static Maybe<TResult> FlatMap<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Maybe<TResult>> mapper
        )
        {
            return source.Match(mapper, none => none);
        }

        public static Maybe<T> Filter<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<T, bool> predicate)
        {
            return source.FlatMap(some => predicate(some) ? Maybe.Some(some) : Maybe<T>.None);
        }

        public static T OrReturn<T>(this Maybe<T> source, T value)
        {
            return source.Match(
                some => some,
                none => value
            );
        }

        public static T OrGet<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<T> value)
        {
            return source.Match(
                some => some,
                none => value()
            );
        }

        public static T OrDefault<T>(this Maybe<T> source)
        {
            return source.Match(
                some => some,
                none => default(T)
            );
        }

        public static T OrThrow<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<Exception> exception)
        {
            if (source.IsNone)
            {
                throw exception();
            }
            return source.OrDefault();
        }

        public static T OrThrow<T>(this Maybe<T> source, [InstantHandle, NotNull] Exception exception)
        {
            if (source.IsNone)
            {
                throw exception;
            }
            return source.OrDefault();
        }

        public static Maybe<TResult> Zip<TFirst, TSecond, TResult>(
            this Maybe<TFirst> first,
            Maybe<TSecond> second,
            [InstantHandle, NotNull] Func<TFirst, TSecond, TResult> zipper
        )
        {
            return first.FlatMap(
                some1 => second.FlatMap(
                    some2 => Maybe.Some(zipper(some1, some2))
                )
            );
        }

        public static Maybe<TResult> Zip<T1, T2, T3, TResult>(
            this Maybe<T1> first,
            Maybe<T2> second,
            Maybe<T3> third,
            [InstantHandle, NotNull] Func<T1, T2, T3, TResult> zipper
        )
        {
            return first.FlatMap(
                some1 => second.FlatMap(
                    some2 => third.FlatMap<T3, TResult>(
                        some3 => zipper(some1, some2, some3)
                    )
                )
            );
        }

        public static Maybe<TResult> Zip<T1, T2, T3, T4, TResult>(
            this Maybe<T1> first,
            Maybe<T2> second,
            Maybe<T3> third,
            Maybe<T4> fourth,
            [InstantHandle, NotNull] Func<T1, T2, T3, T4, TResult> zipper
        )
        {
            return first.FlatMap(
                some1 => second.FlatMap(
                    some2 => third.FlatMap(
                        some3 => fourth.FlatMap<T4, TResult>(
                            some4 => zipper(some1, some2, some3, some4)
                        )
                    )
                )
            );
        }
    }
}