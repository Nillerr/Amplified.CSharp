using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__ToEnumerable
    {
        [Fact]
        public void WhenSome_ReturnsSingleItemEnumerable()
        {
            const int expectedValue = 230023;
            var source = Maybe<int>.Some(expectedValue);
            var result = source.ToEnumerable();
            var value = Assert.Single(result);
            Assert.Equal(expectedValue, value);
        }
        
        [Fact]
        public void WhenNone_ReturnsEmptyEnumerable()
        {
            var source = Maybe<int>.None();
            var result = source.ToEnumerable();
            Assert.Empty(result);
        }
    }
}