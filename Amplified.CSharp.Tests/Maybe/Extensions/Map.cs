using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Map
    {
        [Fact]
        public void OnSome_ReturnsSome_WithMappedResult()
        {
            const long expected = 23000;
            var source = Maybe<int>.Some(1).Map(it => expected);
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnNone_ReturnsNone()
        {
            Maybe<double> source = Maybe<int>.None().Map(it => (double) 690343);
            source.MustBeNone();
        }
    }
}
