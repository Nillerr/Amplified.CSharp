using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__OrAsync
    {
        [Fact]
        public async Task OnSome_ReturnsSource()
        {
            const int expected = 5;
            var source = Maybe<int>.Some(expected);
            var other = AsyncMaybe<int>.Some(79);
            var result = await source.OrAsync(other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public async Task OnSome_ReturnsOther()
        {
            const int expected = 79;
            var source = Maybe<int>.None();
            var other = AsyncMaybe<int>.Some(expected);
            var result = await source.OrAsync(other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public async Task OnSome_WithLambda_ReturnsSource()
        {
            const int expected = 5;
            var source = Maybe<int>.Some(expected);
            var other = AsyncMaybe<int>.Some(79);
            var result = await source.OrAsync(() => other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public async Task OnNone_WithLambda_ReturnsOther()
        {
            const int expected = 79;
            var source = Maybe<int>.None();
            var other = AsyncMaybe<int>.Some(expected);
            var result = await source.OrAsync(() => other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public async Task OnSome_WithMethodReference_ReturnsSource()
        {
            const int expected = 5;
            AsyncMaybe<int> Other() => AsyncMaybe<int>.Some(79);
            
            var source = Maybe<int>.Some(expected);
            var result = await source.OrAsync(Other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public async Task OnNone_WithMethodReference_ReturnsOther()
        {
            const int expected = 79;
            AsyncMaybe<int> Other() => AsyncMaybe<int>.Some(expected);
            
            var source = Maybe<int>.None();
            var result = await source.OrAsync(Other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }
    }
}
