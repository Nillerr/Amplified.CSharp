using System.Threading.Tasks;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__IsNone
    {
        [Fact]
        public async Task None()
        {
            var source = AsyncMaybe<object>.None();
            var isNone = await source.IsNone;
            Assert.True(isNone);
        }
    }
}
