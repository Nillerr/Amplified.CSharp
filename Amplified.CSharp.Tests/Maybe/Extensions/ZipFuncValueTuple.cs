using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__ZipFuncValueTuple
    {
        [Fact]
        public void OnNone_WithOtherNone_ReturnsNone()
        {
            var source = Maybe<int>.None();
            var joined = source.Zip(s => Maybe<int>.None());
            joined.MustBeNone();
        }

        [Fact]
        public void OnSome_WithOtherNone_ReturnsNone()
        {
            var source = Maybe<int>.Some(1);
            var joined = source.Zip(s => Maybe<int>.None());
            joined.MustBeNone();
        }

        [Fact]
        public void OnNone_WithOtherSome_ReturnsNone()
        {
            var source = Maybe<int>.None();
            var joined = source.Zip(s => Maybe<int>.Some(1));
            joined.MustBeNone();
        }

        [Fact]
        public void OnSome_WithOtherSome_ReturnsResultOfJoinFunction()
        {
            var expected = (1, 6);
            var source = Maybe<int>.Some(1);
            var joined = source.Zip(s => Maybe<int>.Some(6));
            var result = joined.MustBeSome();
            Assert.Equal(expected, result);
        }
    }
}