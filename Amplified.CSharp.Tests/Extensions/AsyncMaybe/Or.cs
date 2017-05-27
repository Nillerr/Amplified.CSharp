using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;
using static Amplified.CSharp.Constructors;

namespace Amplified.CSharp
{
    public class Or
    {
        [Fact]
        public async Task Sync_OnSome_WithAsyncOther_ReturnsSelf()
        {
            const int expected = 34;
            var result = await Some(expected).ToAsync().Or(() => Some(545).ToAsync()).OrFail();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Sync_OnSome_WithAsyncStaticOther_ReturnsSelf()
        {
            const int expected = 34;
            var result = await Some(expected).ToAsync().Or(Some(545).ToAsync()).OrFail();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Sync_OnNone_ReturnsOther()
        {
            const int expected = 123;
            var result = await Some(expected).ToAsync().OrReturn(321);
            Assert.Equal(expected, result);
        }
    }
}
