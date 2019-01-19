using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    public class ZipValueTuple
    {
        [Fact]
        public void OnNone_WithOtherNone_ReturnsNone()
        {
            var source = Maybe<int>.None();
            var other = Maybe<int>.None();
            var joined = source.Zip(other);
            joined.MustBeNone();
        }

        [Fact]
        public void OnSome_WithOtherNone_ReturnsNone()
        {
            var source = Maybe<int>.Some(1);
            var other = Maybe<int>.None();
            var joined = source.Zip(other);
            joined.MustBeNone();
        }

        [Fact]
        public void OnNone_WithOtherSome_ReturnsNone()
        {
            var source = Maybe<int>.None();
            var other = Maybe<int>.Some(1);
            var joined = source.Zip(other);
            joined.MustBeNone();
        }

        [Fact]
        public void OnSome_WithOtherSome_ReturnsResultOfJoinFunction()
        {
            var expected = (1, 6);
            var source = Maybe<int>.Some(1);
            var other = Maybe<int>.Some(6);
            var joined = source.Zip(other);
            var result = joined.OrFail();
            Assert.Equal(expected, result);
        }
    }
}