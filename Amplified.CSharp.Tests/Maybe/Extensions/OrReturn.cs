using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__OrReturn
    {
        [Fact]
        public void OnSome_ReturnsValue()
        {
            const int expected = 7;
            var source = Maybe<int>.Some(expected);
            var result = source.OrReturn(0);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnNone_ReturnsInput()
        {
            const int expected = 10045;
            var source = Maybe<int>.None();
            var result = source.OrReturn(expected);
            Assert.Equal(expected, result);
        }
    }
}
