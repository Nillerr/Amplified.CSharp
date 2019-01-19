using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions.Continuations;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
    [DebuggerStepThrough]
    public struct AsyncMaybe<T>
    {
        public static AsyncMaybe<T> None() => default(AsyncMaybe<T>);

        public static AsyncMaybe<T> Some(T value) => new AsyncMaybe<T>(Task.FromResult(Maybe<T>.Some(value)));

        public static AsyncMaybe<T> SomeAsync(Task<T> task) => new AsyncMaybe<T>(task.Then(Maybe<T>.Some));

        [CanBeNull]
        private readonly Task<Maybe<T>> _valueTask;

        public AsyncMaybe(Task<Maybe<T>> valueTask)
        {
            _valueTask = valueTask;
        }

        [NotNull]
        private Task<Maybe<T>> GetTask() => _valueTask ?? Task.FromResult(default(Maybe<T>));

        [NotNull]
        public Task<bool> IsSome => GetTask().Then(it => it.IsSome);

        [NotNull]
        public Task<bool> IsNone => GetTask().Then(it => it.IsNone);

        [NotNull]
        public Task<TResult> Match<TResult>(
            [NotNull] Func<T, TResult> some,
            [NotNull] Func<None, TResult> none
        )
        {
            return GetTask().Then(result => result.Match(some, none));
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [NotNull] Func<T, TResult> some,
            [NotNull] Func<TResult> none
        )
        {
            return GetTask().Then(result => result.Match(some, none));
        }

        [NotNull]
        public Task<TResult> MatchAsync<TResult>(
            [NotNull] Func<T, Task<TResult>> someAsync,
            [NotNull] Func<None, Task<TResult>> noneAsync
        )
        {
            return GetTask().Then(result => result.Match(someAsync, noneAsync));
        }

        [NotNull]
        public Task<TResult> MatchAsync<TResult>(
            [NotNull] Func<T, Task<TResult>> someAsync,
            [NotNull] Func<Task<TResult>> noneAsync
        )
        {
            return GetTask().Then(result => result.Match(someAsync, noneAsync));
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [NotNull] Func<T, Task<TResult>> someAsync,
            [NotNull] Func<None, TResult> none
        )
        {
            return GetTask().Then(result => result.Match(someAsync, n => Task.FromResult(none(n))));
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [NotNull] Func<T, Task<TResult>> someAsync,
            [NotNull] Func<TResult> none
        )
        {
            return GetTask().Then(result => result.Match(someAsync, n => Task.FromResult(none())));
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [NotNull] Func<T, TResult> some,
            [NotNull] Func<None, Task<TResult>> noneAsync
        )
        {
            return GetTask().Then(result => result.Match(s => Task.FromResult(some(s)), noneAsync));
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [NotNull] Func<T, TResult> some,
            [NotNull] Func<Task<TResult>> noneAsync
        )
        {
            return GetTask().Then(result => result.Match(s => Task.FromResult(some(s)), noneAsync));
        }

        [NotNull]
        public Task<Unit> Match(
            [NotNull] Action<T> some,
            [NotNull] Action<None> none
        )
        {
            return GetTask().Then(result => result.Match(some, none));
        }

        [NotNull]
        public Task<Unit> Match(
            [NotNull] Action<T> some,
            [NotNull] Action none
        )
        {
            return GetTask().Then(result => result.Match(some, none));
        }

        [NotNull]
        public Task<Unit> Match(
            [NotNull] Func<T, Task> someAsync,
            [NotNull] Func<None, Task> noneAsync
        )
        {
            return GetTask().Then(
                result => result
                    .Match(
#pragma warning disable 4014
                        some => someAsync(some).WithResult(default(Unit)),
#pragma warning restore 4014
                        none => Task.FromResult(noneAsync(none))
                    )
            );
        }

        [NotNull]
        public Task<Unit> Match(
            [NotNull] Func<T, Task> someAsync,
            [NotNull] Func<Task> noneAsync
        )
        {
            return GetTask().Then(
                result => result
                    .Match(
#pragma warning disable 4014
                        some => someAsync(some).WithResult(default(Unit)),
#pragma warning restore 4014
                        () => Task.FromResult(noneAsync())
                    )
            );
        }

        public static implicit operator AsyncMaybe<T>(Maybe<T> source)
        {
            return new AsyncMaybe<T>(Task.FromResult(source));
        }

        public static implicit operator AsyncMaybe<T>(None none)
        {
            return new AsyncMaybe<T>(Task.FromResult((Maybe<T>) none));
        }

        public TaskAwaiter<Maybe<T>> GetAwaiter()
        {
            return GetTask().GetAwaiter();
        }
    }
}