using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_FlatMap
    {
        [Fact]
        public async Task Sync_ReturningSome_WhenSome_ReturnsResultOfInner()
        {
            const int expected = 5;
            var result = await Some(2).ToAsync().FlatMap(some => Some(some + 3)).OrFail();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Async_ReturningSome_WhenSome_ReturnsResultOfInner()
        {
            const int expected = 5;
            var result = await Some(2).ToAsync().FlatMapAsync(some => Task.FromResult(Some(some + 3))).OrFail();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Sync_ReturningAsyncSome_WhenSome_ReturnsResultOfInner()
        {
            const int expected = 5;
            var result = await Some(2).ToAsync().FlatMap(some => Some(some + 3).ToAsync()).OrFail();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Async_ReturningAsyncSome_WhenSome_ReturnsResultOfInner()
        {
            const int expected = 5;
            var result = await Some(2).ToAsync().FlatMapAsync(some => Task.FromResult(Some(some + 3).ToAsync())).OrFail();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Sync_ReturningSome_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None().FlatMap(some => Some(some + 3)).IsNone;
            Assert.True(isNone);
        }

        [Fact]
        public async Task Async_ReturningSome_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None().FlatMapAsync(some => Task.FromResult(Some(some + 3))).IsNone;
            Assert.True(isNone);
        }

        [Fact]
        public async Task Sync_ReturningAsyncSome_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None().FlatMap(some => Some(some + 3).ToAsync()).IsNone;
            Assert.True(isNone);
        }

        [Fact]
        public async Task Async_ReturningAsyncSome_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None().FlatMapAsync(some => Task.FromResult(Some(some + 3).ToAsync())).IsNone;
            Assert.True(isNone);
        }
        
        #region AsyncMaybe<Unit>.FlatMap(Func<Maybe<T>>)

        [Fact]
        public async Task Sync_ReturningSome_UsingNoArgs_WhenNone_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.None().FlatMapUnit(() => Maybe<int>.Some(3));
            source.MustBeNone();
        }

        [Fact]
        public async Task Sync_ReturningSome_UsingNoArgs_WhenSome_ReturnsSome()
        {
            const int expected = 3;
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnit(() => Maybe<int>.Some(expected));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Sync_ReturningNone_UsingNoArgs_WhenNone_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.None().FlatMapUnit(() => Maybe<int>.Some(3));
            source.MustBeNone();
        }

        [Fact]
        public async Task Sync_ReturningNoneUsingLambda_UsingNoArgs_WhenSome_ReturnsNone()
        {
            // ReSharper disable once ConvertClosureToMethodGroup
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnit(() => Maybe<int>.None());
            source.MustBeNone();
        }

        [Fact]
        public async Task Sync_ReturningNoneUsingMethodRefernece_UsingNoArgs_WhenSome_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnit(Maybe<int>.None);
            source.MustBeNone();
        }
        
        #endregion
        
        #region AsyncMaybe<Unit>.FlatMap(Func<AsyncMaybe<T>>)

        [Fact]
        public async Task Sync_ReturningAsyncSome_UsingNoArgs_WhenNone_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.None().FlatMapUnit(() => AsyncMaybe<int>.Some(3));
            source.MustBeNone();
        }

        [Fact]
        public async Task Sync_ReturningAsyncSome_UsingNoArgs_WhenSome_ReturnsSome()
        {
            const int expected = 3;
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnit(() => AsyncMaybe<int>.Some(expected));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Sync_ReturningAsyncNone_UsingNoArgs_WhenNone_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.None().FlatMapUnit(() => AsyncMaybe<int>.Some(3));
            source.MustBeNone();
        }

        [Fact]
        public async Task Sync_ReturningAsyncNoneUsingLambda_UsingNoArgs_WhenSome_ReturnsNone()
        {
            // ReSharper disable once ConvertClosureToMethodGroup
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnit(() => AsyncMaybe<int>.None());
            source.MustBeNone();
        }

        [Fact]
        public async Task Sync_ReturningAsyncNoneUsingMethodRefernece_UsingNoArgs_WhenSome_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnit(AsyncMaybe<int>.None);
            source.MustBeNone();
        }
        
        #endregion
        
        #region AsyncMaybe<Unit>.FlatMapAsync(Func<Task<Maybe<T>>>))

        [Fact]
        public async Task Async_ReturningSome_UsingNoArgs_WhenNone_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.None().FlatMapUnitAsync(() => Task.FromResult(Maybe<int>.Some(3)));
            source.MustBeNone();
        }

        [Fact]
        public async Task Async_ReturningSome_UsingNoArgs_WhenSome_ReturnsSome()
        {
            const int expected = 3;
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnitAsync(() => Task.FromResult(Maybe<int>.Some(expected)));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Async_ReturningNone_UsingNoArgs_WhenNone_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.None().FlatMapUnitAsync(() => Task.FromResult(Maybe<int>.None()));
            source.MustBeNone();
        }

        [Fact]
        public async Task Async_ReturningNone_UsingNoArgs_WhenSome_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnitAsync(() => Task.FromResult(Maybe<int>.None()));
            source.MustBeNone();
        }
        
        #endregion
        
        #region AsyncMaybe<Unit>.FlatMapAsync(Func<Task<AsyncMaybe<T>>>))

        [Fact]
        public async Task Async_ReturningAsyncSome_UsingNoArgs_WhenNone_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.None().FlatMapUnitAsync(() => Task.FromResult(AsyncMaybe<int>.Some(3)));
            source.MustBeNone();
        }

        [Fact]
        public async Task Async_ReturningAsyncSome_UsingNoArgs_WhenSome_ReturnsSome()
        {
            const int expected = 3;
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnitAsync(() => Task.FromResult(AsyncMaybe<int>.Some(expected)));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Async_ReturningAsyncNone_UsingNoArgs_WhenNone_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.None().FlatMapUnitAsync(() => Task.FromResult(AsyncMaybe<int>.None()));
            source.MustBeNone();
        }

        [Fact]
        public async Task Async_ReturningAsyncNone_UsingNoArgs_WhenSome_ReturnsNone()
        {
            var source = await AsyncMaybe<Unit>.Some(new Unit()).FlatMapUnitAsync(() => Task.FromResult(AsyncMaybe<int>.None()));
            source.MustBeNone();
        }
        
        #endregion
    }
}
