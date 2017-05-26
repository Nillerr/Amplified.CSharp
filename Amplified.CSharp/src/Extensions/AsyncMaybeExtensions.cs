using System;
using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeExtensions
    {
        public static AsyncMaybe<TResult> Map<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<TResult>> mapperAsync)
        {
            return source.Match(some => mapperAsync(some).Then(Some), none => Maybe<TResult>.None).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> Map<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, TResult> mapper)
        {
            return source.Match(some => Some(mapper(some)), none => Maybe<TResult>.None).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> FlatMap<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Maybe<TResult>> mapper)
        {
            return source.Match(mapper, none => Maybe<TResult>.None).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> FlatMap<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<Maybe<TResult>>> mapper)
        {
            return source.Match(mapper, none => Maybe<TResult>.None).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> FlatMap<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, AsyncMaybe<TResult>> mapper)
        {
            return source.Match(mapper, none => AsyncMaybe<TResult>.None).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> FlatMap<T, TResult>(
            this AsyncMaybe<T> source,
            [NotNull] Func<T, Task<AsyncMaybe<TResult>>> mapper)
        {
            return source.Match(mapper, none => AsyncMaybe<TResult>.None).ToAsyncMaybe();
        }

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

        public static AsyncMaybe<T> Filter<T>(
            this AsyncMaybe<T> source,
            [InstantHandle, NotNull] Func<T, bool> predicate)
        {
            return source.Match(
                some => predicate(some)
                    ? Some(some)
                    : Maybe<T>.None,
                none => Maybe<T>.None
            ).ToAsyncMaybe();
        }

        public static AsyncMaybe<T> Filter<T>(
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

        public static Task<T> OrReturn<T>(this AsyncMaybe<T> source, T value)
        {
            return source.Match(
                some => some,
                none => value
            );
        }

        public static Task<T> OrReturn<T>(this AsyncMaybe<T> source, Task<T> value)
        {
            return source.Match(
                some => some,
                none => value
            );
        }

        public static Task<T> OrGet<T>(this AsyncMaybe<T> source, [InstantHandle, NotNull] Func<T> value)
        {
            return source.Match(
                some => some,
                none => value()
            );
        }

        public static Task<T> OrGet<T>(this AsyncMaybe<T> source, [InstantHandle, NotNull] Func<Task<T>> value)
        {
            return source.Match(
                some => some,
                none => value()
            );
        }

        public static Task<T> OrDefault<T>(this AsyncMaybe<T> source)
        {
            return source.Match(
                some => some,
                none => default(T)
            );
        }

        public static Task<T> OrThrow<T>(this AsyncMaybe<T> source, [InstantHandle, NotNull] Func<Exception> exception)
        {
            return source.Match(
                some => some,
                none: _ => throw exception()
            );
        }

        public static async Task<T> OrThrow<T>(this AsyncMaybe<T> source, [InstantHandle, NotNull] Func<Task<Exception>> exception)
        {
            return await source.Match<T>(
                some: some => some,
                noneAsync: async _ => throw await exception() 
            );
        }

        public static async Task<T> OrThrow<T>(this AsyncMaybe<T> source, [NotNull] Task<Exception> exception)
        {
            return await source.Match<T>(
                some: some => some,
                noneAsync: async _ => throw await exception
            );
        }

        public static Task<T> OrThrow<T>(this AsyncMaybe<T> source, [NotNull] Exception exception)
        {
            return source.Match(
                some => some,
                none: _ => throw exception
            );
        }

        public static AsyncMaybe<T> Or<T>(this AsyncMaybe<T> source, AsyncMaybe<T> other)
        {
            return source.Match(
                some: Some,
                noneAsync: none => other.ToTask()
            ).ToAsyncMaybe();
        }

        public static AsyncMaybe<T> Or<T>(this AsyncMaybe<T> source, [InstantHandle, NotNull] Func<AsyncMaybe<T>> other)
        {
            return source.Match(
                some: Some,
                noneAsync: none => other().ToTask()
            ).ToAsyncMaybe();
        }

        public static AsyncMaybe<TResult> Zip<T1, T2, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            [InstantHandle, NotNull] Func<T1, T2, TResult> zipper
        )
        {
            return first.FlatMap(
                some1 => second.Map(
                    some2 => zipper(some1, some2)
                )
            );
        }

        public static AsyncMaybe<TResult> Zip<T1, T2, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            [InstantHandle, NotNull] Func<T1, T2, Task<TResult>> zipper
        )
        {
            return first.FlatMap(
                some1 => second.Map(
                    some2 => zipper(some1, some2)
                )
            );
        }

        public static AsyncMaybe<TResult> Zip<T1, T2, T3, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            AsyncMaybe<T3> third,
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

        public static AsyncMaybe<TResult> Zip<T1, T2, T3, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            AsyncMaybe<T3> third,
            [InstantHandle, NotNull] Func<T1, T2, T3, Task<TResult>> zipper
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

        public static AsyncMaybe<TResult> Zip<T1, T2, T3, T4, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            AsyncMaybe<T3> third,
            AsyncMaybe<T4> fourth,
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

        public static AsyncMaybe<TResult> Zip<T1, T2, T3, T4, TResult>(
            this AsyncMaybe<T1> first,
            AsyncMaybe<T2> second,
            AsyncMaybe<T3> third,
            AsyncMaybe<T4> fourth,
            [InstantHandle, NotNull] Func<T1, T2, T3, T4, Task<TResult>> zipper
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