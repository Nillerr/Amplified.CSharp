using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;
using static Amplified.CSharp.Constructors;

namespace Amplified.CSharp
{
    public class OrThrow
    {
        [Fact]
        public async Task Sync_OnSome_WithFunction_ReturnsResultOfSome()
        {
            const int expected = 123132;
            var source = Some(expected).ToAsync();
            var result = await source.OrThrow(() => new DummyException());
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Sync_OnNone_WithFunction_ThrowsException()
        {
            var source = AsyncMaybe<int>.None;
            await Assert.ThrowsAsync<DummyException>(() => source.OrThrow(() => new DummyException()));
        }
        
        [Fact]
        public async Task Async_OnSome_WithFunction_ReturnsResultOfSome()
        {
            const int expected = 123132;
            var source = Some(expected).ToAsync();
            var result = await source.OrThrow(() => new DummyException());
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Async_OnNone_WithFunction_ThrowsException()
        {
            var source = AsyncMaybe<int>.None;
            await Assert.ThrowsAsync<DummyException>(() => source.OrThrow(() => new DummyException()));
        }
    }
}
