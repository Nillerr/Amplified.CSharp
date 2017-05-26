using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    public class OrGet
    {
        [Fact]
        public async Task Sync_OnSome_ReturnsResultOfSome()
        {
            const int expected = 123;
            var result = await Some(expected).ToAsync().OrGet(() => 321);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Sync_OnNone_ReturnsResultOfFunction()
        {
            const int expected = 321;
            var result = await Maybe<int>.None.ToAsync().OrGet(() => expected);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Async_OnSome_ReturnsResultOfSome()
        {
            const int expected = 123;
            var result = await Some(expected).ToAsync().OrGetAsync(async () => await Task.FromResult(321));
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Async_OnNone_ReturnsResultOfFunction()
        {
            const int expected = 321;
            var result = await Maybe<int>.None.ToAsync().OrGetAsync(async () => await Task.FromResult(expected));
            Assert.Equal(expected, result);
        }
    }
}
