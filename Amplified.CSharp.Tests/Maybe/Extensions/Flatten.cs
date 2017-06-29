using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Flatten
    {
        [Fact]
        public void OnSomeSome_ReturnsSome()
        {
            const int expected = 1000;
            var source = Maybe<Maybe<int>>.Some(Maybe<int>.Some(expected)).Flatten();
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnSomeNone_ReturnsNone()
        {
            var source = Maybe<Maybe<int>>.Some(Maybe<int>.None()).Flatten();
            source.MustBeNone();
        }

        [Fact]
        public void OnNone_ReturnsNone()
        {
            var source = Maybe<Maybe<int>>.None().Flatten();
            source.MustBeNone();
        }
    }
}
