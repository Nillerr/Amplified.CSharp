using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Or
    {
        [Fact]
        public void OnSome_ReturnsSource()
        {
            const int expected = 5;
            var source = Maybe<int>.Some(expected);
            var other = Maybe<int>.Some(79);
            var result = source.Or(other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public void OnNone_ReturnsOther()
        {
            const int expected = 79;
            var source = Maybe<int>.None();
            var other = Maybe<int>.Some(expected);
            var result = source.Or(other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public void OnSome_WithLambda_ReturnsSource()
        {
            const int expected = 5;
            var source = Maybe<int>.Some(expected);
            var other = Maybe<int>.Some(79);
            var result = source.Or(() => other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public void OnNone_WithLambda_ReturnsOther()
        {
            const int expected = 79;
            var source = Maybe<int>.None();
            var other = Maybe<int>.Some(expected);
            var result = source.Or(() => other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public void OnSome_WithLambdaAndArgument_ReturnsSource()
        {
            const int expected = 5;
            var source = Maybe<int>.Some(expected);
            var other = Maybe<int>.Some(79);
            var result = source.Or((None none) => other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public void OnNone_WithLambdaAndArgument_ReturnsOther()
        {
            const int expected = 79;
            var source = Maybe<int>.None();
            var other = Maybe<int>.Some(expected);
            var result = source.Or((None none) => other);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }
    }
}
