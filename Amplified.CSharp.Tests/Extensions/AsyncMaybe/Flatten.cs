using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    public class Flatten
    {
        [Fact]
        public async Task WithSome_ReturnsResultOfSome()
        {
            const int expected = 3;
            var source = await Some(1).ToAsync().Map(it => Some(3)).Flatten().OrFail();
            Assert.Equal(expected, source);
        }
        
        [Fact]
        public async Task WithAsyncSome_ReturnsResultOfSome()
        {
            const int expected = 3;
            var source = await Some(1).ToAsync().Map(it => Some(3).ToAsync()).Flatten().OrFail();
            Assert.Equal(expected, source);
        }
    }
}
