using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_MapAsync
    {
        #region AsyncMaybe<TResult> MapAsync<TResult>(Func<T, Task<TResult>> mapperAsync)
        
        [Fact]
        public async Task Async_WhenSome_ReturnsMappedResult()
        {
            const int expected = 5;
            var result = await AsyncMaybe<int>.Some(2).MapAsync(some => Task.FromResult(some + 3)).OrFail();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Async_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None().MapAsync(some => Task.FromResult(some + 3)).IsNone;
            Assert.True(isNone);
        }
        
        #endregion

        #region AsyncMaybe<Unit> MapAsync<TResult>(Func<T, Task> mapper)

        [Fact]
        public async Task Some_Map_Action_T_Lambda_ReturnsSomeUnit()
        {
            var source = AsyncMaybe<int>.Some(5);
            var result = await source.MapAsync(some => Task.CompletedTask);
            var unit = result.MustBeSome();
            Assert.IsType<Unit>(unit);
        }

        [Fact]
        public async Task Some_Map_Action_T_MethodReference_ReturnsSomeUnit()
        {
            Task Foo(int it) => Task.CompletedTask;
            var source = AsyncMaybe<int>.Some(5);
            var result = await source.MapAsync(Foo);
            var unit = result.MustBeSome();
            Assert.IsType<Unit>(unit);
        }

        [Fact]
        public async Task Some_Map_Action_T__ActionIsInvokedOnlyOnce()
        {
            var rec = new Recorder();
            var source = AsyncMaybe<int>.Some(5);
            var result = await source.MapAsync(rec.Record((int it) => Task.CompletedTask));
            result.MustBeSome();
            rec.MustHaveExactly(1.Invocations());
        }

        [Fact]
        public async Task None_Map_Action_T__ActionIsNotInvoked()
        {
            var rec = new Recorder();
            var source = AsyncMaybe<int>.None();
            var result = await source.MapAsync(rec.Record((int it) => Task.CompletedTask));
            result.MustBeNone();
            rec.MustHaveExactly(0.Invocations());
        }

        [Fact]
        public async Task None_Map_Action_T__ReturnsNoneUnit()
        {
            var source = AsyncMaybe<int>.None();
            var result = await source.MapAsync(it => Task.CompletedTask);
            result.MustBeNone();
        }
        
        #endregion

        #region AsyncMaybe<Unit> MapAsync<TResult>(Func<T, Task> mapper)

        [Fact]
        public async Task Some_Map_ActionLambda_ReturnsSomeUnit()
        {
            var source = AsyncMaybe<int>.Some(5);
            var result = await source.MapAsync(() => Task.CompletedTask);
            var unit = result.MustBeSome();
            Assert.IsType<Unit>(unit);
        }

        [Fact]
        public async Task Some_Map_ActionMethodReference_ReturnsSomeUnit()
        {
            Task Foo() => Task.CompletedTask;
            var source = AsyncMaybe<int>.Some(5);
            var result = await source.MapAsync(Foo);
            var unit = result.MustBeSome();
            Assert.IsType<Unit>(unit);
        }

        [Fact]
        public async Task Some_Map_Action_ActionIsInvokedOnlyOnce()
        {
            var rec = new Recorder();
            var source = AsyncMaybe<int>.Some(5);
            var result = await source.MapAsync(rec.Record(() => Task.CompletedTask));
            result.MustBeSome();
            rec.MustHaveExactly(1.Invocations());
        }

        [Fact]
        public async Task None_Map_Action_ActionIsNotInvoked()
        {
            var rec = new Recorder();
            var source = AsyncMaybe<int>.None();
            var result = await source.MapAsync(rec.Record(() => Task.CompletedTask));
            result.MustBeNone();
            rec.MustHaveExactly(0.Invocations());
        }

        [Fact]
        public async Task None_Map_Action_ReturnsNoneUnit()
        {
            var source = AsyncMaybe<int>.None();
            var result = await source.MapAsync(() => Task.CompletedTask);
            result.MustBeNone();
        }
        
        #endregion
    }
}