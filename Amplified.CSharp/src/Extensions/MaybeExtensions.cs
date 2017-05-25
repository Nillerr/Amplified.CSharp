using System;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeExtensions
    {
        public static Maybe<TResult> Map<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, TResult> mapper)
        {
            return source.Match(
                some => Some(mapper(some)),
                none => Maybe<TResult>.None
            );
        }

        public static Maybe<TResult> FlatMap<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Maybe<TResult>> mapper
        )
        {
            return source.Match(mapper, none => Maybe<TResult>.None);
        }

        public static Maybe<T> Filter<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<T, bool> predicate)
        {
            return source.FlatMap(some => predicate(some) ? Some(some) : Maybe<T>.None);
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
            return source.Match(
                some => some,
                none => throw exception()
            );
        }

        public static T OrThrow<T>(this Maybe<T> source, [NotNull] Exception exception)
        {
            return source.Match(
                some => some,
                none => throw exception
            );
        }

        public static Maybe<T> Or<T>(this Maybe<T> source, Maybe<T> other)
        {
            return source.Match(Some, none => other);
        }

        public static Maybe<T> Or<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<Maybe<T>> other)
        {
            return source.Match(Some, none => other());
        }

        public static Maybe<T> Or<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<None, Maybe<T>> other)
        {
            return source.Match(Some, other);
        }

        public static Maybe<TResult> Zip<T1, T2, TResult>(
            this Maybe<T1> first,
            Maybe<T2> second,
            [InstantHandle, NotNull] Func<T1, T2, TResult> zipper
        )
        {
            return first.FlatMap<T1, TResult>(
                some1 => second.Map<T2, TResult>(
                    some2 => zipper(some1, some2)
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
            return first.FlatMap<T1, TResult>(
                some1 => second.FlatMap<T2, TResult>(
                    some2 => third.Map<T3, TResult>(
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
            return first.FlatMap<T1, TResult>(
                some1 => second.FlatMap<T2, TResult>(
                    some2 => third.FlatMap<T3, TResult>(
                        some3 => fourth.Map<T4, TResult>(
                            some4 => zipper(some1, some2, some3, some4)
                        )
                    )
                )
            );
        }
    }
}