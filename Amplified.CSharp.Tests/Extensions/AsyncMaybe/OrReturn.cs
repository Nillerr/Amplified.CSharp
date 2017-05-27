using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;
using static Amplified.CSharp.Constructors;

namespace Amplified.CSharp
{
    public class OrReturn
    {
        [Fact]
        public async Task Sync_OnSome_ReturnsResultOfSome()
        {
            const int expected = 123;
            var result = await Some(expected).ToAsync().OrReturn(321);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Sync_OnNone_ReturnsResultOfValue()
        {
            const int expected = 321;
            var result = await Maybe<int>.None.ToAsync().OrReturn(expected);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Async_OnSome_ReturnsResultOfSome()
        {
            const int expected = 123;
            var result = await Some(expected).ToAsync().OrReturnAsync(Task.FromResult(321));
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Async_OnNone_ReturnsResultOfValue()
        {
            const int expected = 321;
            var result = await Maybe<int>.None.ToAsync().OrReturnAsync(Task.FromResult(expected));
            Assert.Equal(expected, result);
        }
    }
}
