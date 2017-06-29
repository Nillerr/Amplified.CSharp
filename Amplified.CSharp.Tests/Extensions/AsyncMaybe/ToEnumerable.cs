using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__ToEnumerable
    {
        [Fact]
        public async Task WhenSome_ReturnsSingleItemEnumerable()
        {
            const int expectedValue = 230023;
            var source = AsyncMaybe<int>.Some(expectedValue);
            var result = await source.ToEnumerable();
            var value = Assert.Single(result);
            Assert.Equal(expectedValue, value);
        }
        
        [Fact]
        public async Task WhenNone_ReturnsEmptyEnumerable()
        {
            var source = AsyncMaybe<int>.None();
            var result = await source.ToEnumerable();
            Assert.Empty(result);
        }
    }
}