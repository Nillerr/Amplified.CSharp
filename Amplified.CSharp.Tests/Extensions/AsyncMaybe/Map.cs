using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_Map
    {
        [Fact]
        public async Task Sync_WhenSome_ReturnsMappedResult()
        {
            const int expected = 5;
            var result = await Some(2).ToAsync().Map(some => some + 3).OrFail();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Async_WhenSome_ReturnsMappedResult()
        {
            const int expected = 5;
            var result = await Some(2).ToAsync().MapAsync(some => Task.FromResult(some + 3)).OrFail();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Sync_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None().Map(some => some + 3).IsNone;
            Assert.True(isNone);
        }

        [Fact]
        public async Task Async_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None().MapAsync(some => Task.FromResult(some + 3)).IsNone;
            Assert.True(isNone);
        }
    }
}
