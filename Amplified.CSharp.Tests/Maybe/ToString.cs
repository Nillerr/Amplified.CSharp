using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__ToString
    {
        [Fact]
        public void WhenNone_ReturnsEmptyString()
        {
            var source = Maybe<int>.None();
            var str = source.ToString();
            Assert.Equal(string.Empty, str);
        }
        
        [Fact]
        public void WhenSome_ReturnsToStringOfValue()
        {
            var value = 1;
            var expected = value.ToString();
            var source = Maybe<int>.Some(value);
            var str = source.ToString();
            Assert.Equal(expected, str);
        }
    }
}
