using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    public class FlatMap
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
            var result = await Some(2).ToAsync().FlatMap(some => Task.FromResult(Some(some + 3))).OrFail();
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
            var result = await Some(2).ToAsync().FlatMap(some => Task.FromResult(Some(some + 3).ToAsync())).OrFail();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Sync_ReturningSome_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None.FlatMap(some => Some(some + 3)).IsNone;
            Assert.True(isNone);
        }

        [Fact]
        public async Task Async_ReturningSome_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None.FlatMap(some => Task.FromResult(Some(some + 3))).IsNone;
            Assert.True(isNone);
        }

        [Fact]
        public async Task Sync_ReturningAsyncSome_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None.FlatMap(some => Some(some + 3).ToAsync()).IsNone;
            Assert.True(isNone);
        }

        [Fact]
        public async Task Async_ReturningAsyncSome_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None.FlatMap(some => Task.FromResult(Some(some + 3)).ToAsyncMaybe()).IsNone;
            Assert.True(isNone);
        }
    }
}
