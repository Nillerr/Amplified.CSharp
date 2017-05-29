using System.Threading.Tasks;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__IsSome
    {
        [Fact]
        public async Task Some()
        {
            var source = AsyncMaybe<object>.Some(1);
            var isSome = await source.IsSome;
            Assert.True(isSome);
        }
        
        [Fact]
        public async Task SomeAsync()
        {
            var source = AsyncMaybe<object>.SomeAsync(Task.FromResult(new object()));
            var isSome = await source.IsSome;
            Assert.True(isSome);
        }
    }
}
