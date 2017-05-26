using System;
using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;
using JetBrains.Annotations;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeToAsyncMaybeExtensions
    {
        public static AsyncMaybe<T> ToAsync<T>(this Maybe<T> source)
        {
            return new AsyncMaybe<T>(Task.FromResult(source));
        }
        
        public static AsyncMaybe<TResult> MapAsync<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Task<TResult>> mapper)
        {
            return source.Match(
                some => mapper(some).Then(Some),
                none => Task.FromResult(Maybe<TResult>.None)
            ).ToAsyncMaybe();
        }
        
        public static AsyncMaybe<TResult> FlatMapAsync<T, TResult>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Task<Maybe<TResult>>> mapper)
        {
            return source.Match(
                mapper,
                none => Task.FromResult(Maybe<TResult>.None)
            ).ToAsyncMaybe();
        }
        
        public static AsyncMaybe<T> FilterAsync<T>(
            this Maybe<T> source,
            [InstantHandle, NotNull] Func<T, Task<bool>> predicate)
        {
            return source.Match(
                some => predicate(some).Then(matches => matches ? Some(some) : Maybe<T>.None),
                none => Task.FromResult(Maybe<T>.None)
            ).ToAsyncMaybe();
        }

        public static Task<T> OrReturnAsync<T>(this Maybe<T> source, Task<T> other)
        {
            return source.Match(
                Task.FromResult,
                none => other
            );
        }

        public static Task<T> OrGetAsync<T>(this Maybe<T> source, Func<Task<T>> other)
        {
            return source.Match(Task.FromResult, none => other());
        }

        public static async Task<T> OrThrowAsync<T>(this Maybe<T> source, Func<Task<Exception>> other)
        {
            return await source.Match<Task<T>>(
                Task.FromResult, 
                none => other().Then<Exception, T>(continuation: ex => throw ex)
            );
        }

        public static AsyncMaybe<T> OrAsync<T>(this Maybe<T> source, AsyncMaybe<T> other)
        {
            return source.Match(
                some => new AsyncMaybe<T>(Task.FromResult(source)),
                none => other
            );
        }

        public static AsyncMaybe<T> OrAsync<T>(this Maybe<T> source, Func<AsyncMaybe<T>> other)
        {
            return source.Match(
                some => new AsyncMaybe<T>(Task.FromResult(source)),
                none => other()
            );
        }
    }
}
