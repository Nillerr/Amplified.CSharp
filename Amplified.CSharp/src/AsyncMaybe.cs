using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
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
        public Task<bool> IsSome => _valueTask?.Then(it => it.IsSome) ?? Task.FromResult(false);

        [NotNull]
        public Task<bool> IsNone => _valueTask?.Then(it => it.IsNone) ?? Task.FromResult(false);

        [NotNull]
        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<None, TResult> none
        )
        {
            return _valueTask?.Then(result => result.Match(some, none)) ?? Task.FromResult(none(default(None)));
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<TResult> none
        )
        {
            return _valueTask?.Then(result => result.Match(some, none)) ?? Task.FromResult(none());
        }

        [NotNull]
        public Task<TResult> MatchAsync<TResult>(
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<None, Task<TResult>> noneAsync
        )
        {
            return _valueTask?.Then(result => result.Match(someAsync, noneAsync)) ?? noneAsync(default(None));
        }

        [NotNull]
        public Task<TResult> MatchAsync<TResult>(
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<Task<TResult>> noneAsync
        )
        {
            return _valueTask?.Then(result => result.Match(someAsync, noneAsync)) ?? noneAsync();
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<None, TResult> none
        )
        {
            return _valueTask?.Then(result => result.Match(someAsync, n => Task.FromResult(none(n)))) ??
                   Task.FromResult(none(default(None)));
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<TResult> none
        )
        {
            return _valueTask?.Then(result => result.Match(someAsync, n => Task.FromResult(none()))) ??
                   Task.FromResult(none());
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<None, Task<TResult>> noneAsync
        )
        {
            return _valueTask?.Then(result => result.Match(s => Task.FromResult(some(s)), noneAsync)) ??
                   noneAsync(default(None));
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<Task<TResult>> noneAsync
        )
        {
            return _valueTask?.Then(result => result.Match(s => Task.FromResult(some(s)), noneAsync)) ?? noneAsync();
        }

        [NotNull]
        public Task<Unit> Match(
            [InstantHandle, NotNull] Action<T> some,
            [InstantHandle, NotNull] Action<None> none
        )
        {
            return _valueTask?.Then(result => result.Match(some, none)) ?? Task.FromResult(default(Unit));
        }

        [NotNull]
        public Task<Unit> Match(
            [InstantHandle, NotNull] Action<T> some,
            [InstantHandle, NotNull] Action none
        )
        {
            return _valueTask?.Then(result => result.Match(some, none)) ?? Task.FromResult(default(Unit));
        }

        [NotNull]
        public Task<Unit> Match(
            [InstantHandle, NotNull] Func<T, Task> someAsync,
            [InstantHandle, NotNull] Func<None, Task> noneAsync
        )
        {
            return _valueTask?.Then(
                       result => result
                           .Match(
                               async some => await someAsync(some).WithResult(default(Unit)),
                               none => Task.FromResult(noneAsync(none))
                           )
                   ) ?? Task.FromResult(default(Unit));
        }

        [NotNull]
        public Task<Unit> Match(
            [InstantHandle, NotNull] Func<T, Task> someAsync,
            [InstantHandle, NotNull] Func<Task> noneAsync
        )
        {
            return _valueTask?.Then(
                       result => result
                           .Match(
                               async some => await someAsync(some).WithResult(default(Unit)),
                               () => Task.FromResult(noneAsync())
                           )
                   ) ?? Task.FromResult(default(Unit));
        }

        public static implicit operator AsyncMaybe<T>(Maybe<T> source)
        {
            return new AsyncMaybe<T>(Task.FromResult(source));
        }

        public static implicit operator AsyncMaybe<T>(None none)
        {
            return new AsyncMaybe<T>(Task.FromResult(Maybe<T>.None()));
        }

        public TaskAwaiter<Maybe<T>> GetAwaiter()
        {
            return _valueTask?.GetAwaiter() ?? Task.FromResult(default(Maybe<T>)).GetAwaiter();
        }
    }
}