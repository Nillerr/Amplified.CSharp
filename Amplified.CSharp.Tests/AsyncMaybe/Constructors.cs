using System.Threading.Tasks;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Constructors
    {
        [Fact]
        public async Task None()
        {
            var source = await AsyncMaybe<int>.None();
            source.MustBeNone();
        }
        
        [Fact]
        public async Task Some()
        {
            var result = await AsyncMaybe<int>.Some(133).OrFail();
            Assert.Equal(133, result);
        }
        
        [Fact]
        public async Task SomeAsync()
        {
            var result = await AsyncMaybe<int>.SomeAsync(Task.FromResult(134)).OrFail();
            Assert.Equal(134, result);
        }
    }
}
