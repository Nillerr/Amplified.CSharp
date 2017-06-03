using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__SelectAsync
    {
        [Fact]
        public async Task OnSome_ReturnsSameAsMapAsync()
        {
            const int value = 3;
            var source = Maybe<int>.Some(value);
            
            var whered = await source.SelectAsync(v => Task.FromResult(v + 1));
            var filtered = await source.MapAsync(v => Task.FromResult(v + 1));

            Assert.Equal(whered, filtered);
        }
        
        [Fact]
        public async Task OnNone_ReturnsSameAsMapAsync()
        {
            var source = Maybe<int>.None();
            
            var whered = await source.SelectAsync(v => Task.FromResult(true));
            var filtered = await source.MapAsync(v => Task.FromResult(true));

            Assert.Equal(whered, filtered);
        }
    }
}
