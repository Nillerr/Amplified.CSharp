using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    public class AsyncMaybeTests
    {
        [Fact]
        public async Task CallingOr_OnNone_ReturnsOther()
        {
            AsyncMaybe<int> Other() => Some(3).ToAsync();

            var shouldBeOther = await None()
                .ToAsyncMaybe<int>()
                .Or(Other);

            var result = shouldBeOther.OrFail();

            Assert.Equal(result, 3);
        }

        [Fact]
        public async Task MaybeToAsyncMaybe()
        {
            const int value = 2;
            var result = await new Maybe<int>(value).ToAsync().OrFail();
            Assert.Equal(result, value);
        }
    }
}
