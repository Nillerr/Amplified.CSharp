using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__WhereAsyncAsync
    {
        [Fact]
        public async Task OnSome_WithTruePredicate_ReturnsSameAsFilterAsync()
        {
            const int value = 3;
            var source = Maybe<int>.Some(value);
            
            var whered = await source.WhereAsync(v => Task.FromResult(true));
            var filtered = await source.FilterAsync(v => Task.FromResult(true));

            Assert.Equal(whered, filtered);
        }
        
        [Fact]
        public async Task OnSome_WithFalsePredicate_ReturnsSameAsFilterAsync()
        {
            const int value = 3;
            var source = Maybe<int>.Some(value);
            
            var whered = await source.WhereAsync(v => Task.FromResult(false));
            var filtered = await source.FilterAsync(v => Task.FromResult(false));

            Assert.Equal(whered, filtered);
        }
        
        [Fact]
        public async Task OnNone_WithTruePredicate_ReturnsSameAsFilterAsync()
        {
            var source = Maybe<int>.None();
            
            var whered = await source.WhereAsync(v => Task.FromResult(true));
            var filtered = await source.FilterAsync(v => Task.FromResult(true));

            Assert.Equal(whered, filtered);
        }
        
        [Fact]
        public async Task OnNone_WithFalsePredicate_ReturnsSameAsFilterAsync()
        {
            var source = Maybe<int>.None();
            
            var whered = await source.WhereAsync(v => Task.FromResult(false));
            var filtered = await source.FilterAsync(v => Task.FromResult(false));

            Assert.Equal(whered, filtered);
        }
    }
}
