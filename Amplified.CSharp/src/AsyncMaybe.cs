using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
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
            [InstantHandle, NotNull] Func<T, Task<TResult>> someAsync,
            [InstantHandle, NotNull] Func<None, Task<TResult>> noneAsync
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
            [InstantHandle, NotNull] Func<T, TResult> some,
            [InstantHandle, NotNull] Func<None, Task<TResult>> noneAsync
        )
        {
            return _valueTask.Then(result => result.Match(s => Task.FromResult(some(s)), noneAsync));
        }

        public TaskAwaiter<Maybe<T>> GetAwaiter()
        {
            return _valueTask.GetAwaiter();
        }
    }

    public static class TestClass
    {
        public static async Task Main(Maybe<int> mt)
        {
            var result = await mt
                .ToAsync()
                .Map(it => ResultAsync(it, 8L))
                .Match(
                    some => ResultAsync(some, 8L),
                    none => 9L
                );
        }

        public static Task<TResult> ResultAsync<T, TResult>(T input, TResult result)
        {
            return Task.FromResult(result);
        }
    }

    public static class AsyncResult
    {
        public static AsyncResult<T> FromTask<T>(Func<Task<T>> task)
        {
            return task();
        }
    }

    public struct AsyncResult<T>
    {
        private readonly Task<T> _task;

        public AsyncResult(Task<T> task)
        {
            _task = task;
        }

        public static implicit operator AsyncResult<T>(T value)
        {
            return new AsyncResult<T>(Task.FromResult(value));
        }

        public static implicit operator AsyncResult<T>(Task<T> task)
        {
            return new AsyncResult<T>(task);
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return _task.GetAwaiter();
        }

        public Task<T> ToTask() => _task;
    }
}