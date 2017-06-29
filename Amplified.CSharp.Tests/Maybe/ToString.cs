using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__ToString
    {
        [Fact]
        public void WhenNone_ReturnsNameOfNoneType()
        {
            var source = Maybe<int>.None();
            var str = source.ToString();
            Assert.Equal(nameof(None), str);
        }
        
        [Fact]
        public void WhenSome_ReturnsValueWrappedInSome()
        {
            var value = 1;
            var expected = $"{nameof(Maybe<int>.Some)}({value})";
            var source = Maybe<int>.Some(value);
            var str = source.ToString();
            Assert.Equal(expected, str);
        }
    }
}
