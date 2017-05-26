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

        private readonly Task<Maybe<T>> _valueTask;

        public AsyncMaybe(Task<Maybe<T>> valueTask)
        {
            _valueTask = valueTask;
        }

        public Task<bool> IsSome => _valueTask.Then(it => it.IsSome);

        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<None, TResult> none
        )
        {
            return _valueTask.Then(result => result.Match(some, none));
        }

        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<TResult> none
        )
        {
            return _valueTask.Then(result => result.Match(some, none));
        }

        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<None, Task<TResult>> noneAsync
        )
        {
            return _valueTask.Then(result => result.Match(someAsync, noneAsync));
        }

        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<Task<TResult>> noneAsync
        )
        {
            return _valueTask.Then(result => result.Match(someAsync, noneAsync));
        }

        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<None, TResult> none
        )
        {
            return _valueTask.Then(result => result.Match(someAsync, n => Task.FromResult(none(n))));
        }

        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<TResult> none
        )
        {
            return _valueTask.Then(result => result.Match(someAsync, n => Task.FromResult(none())));
        }

        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<None, Task<TResult>> noneAsync
        )
        {
            return _valueTask.Then(result => result.Match(s => Task.FromResult(some(s)), noneAsync));
        }

        public Task<TResult> Match<TResult>(
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<Task<TResult>> noneAsync
        )
        {
            return _valueTask.Then(result => result.Match(s => Task.FromResult(some(s)), noneAsync));
        }

        public TaskAwaiter<Maybe<T>> GetAwaiter()
        {
            return _valueTask.GetAwaiter();
        }
    }
}