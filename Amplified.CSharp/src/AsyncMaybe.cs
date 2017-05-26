using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
    public struct AsyncMaybe<T>
    {
        public static AsyncMaybe<T> None = default(AsyncMaybe<T>);

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
        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<None, Task<TResult>> noneAsync
        )
        {
            return _valueTask?.Then(result => result.Match(someAsync, noneAsync)) ?? noneAsync(default(None));
        }

        [NotNull]
        public Task<TResult> Match<TResult>(
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

        public static implicit operator AsyncMaybe<T>(Maybe<T> source)
        {
            return new AsyncMaybe<T>(Task.FromResult(source));
        }

        public TaskAwaiter<Maybe<T>> GetAwaiter()
        {
            return _valueTask?.GetAwaiter() ?? Task.FromResult(default(Maybe<T>)).GetAwaiter();
        }
    }
}