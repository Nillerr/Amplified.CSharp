using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__ToAsyncMaybe
    {
        [Fact]
        public async Task ToAsyncMaybe_ReturnsNone()
        {
            var source = default(None);
            
            var result = source.ToAsyncMaybe<int>();
            Assert.IsType<AsyncMaybe<int>>(result);
            
            var maybe = await result;
            maybe.MustBeNone();
        }

        [Fact]
        public async Task ToSomeAsync_WithConstant_ReturnsSome()
        {
            var source = default(None);
            
            var result = source.ToSomeAsync(Task.FromResult(23));
            Assert.IsType<AsyncMaybe<int>>(result);

            var maybe = await result;
            var value = maybe.MustBeSome();
            Assert.Equal(23, value);
        }

        [Fact]
        public async Task ToSomeAsync_WithProvider_ReturnsSome()
        {
            var source = default(None);
            
            var result = source.ToSomeAsync(() => Task.FromResult(23));
            Assert.IsType<AsyncMaybe<int>>(result);

            var maybe = await result;
            var value = maybe.MustBeSome();
            Assert.Equal(23, value);
        }
    }
}