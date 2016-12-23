using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    public class MaybeExtensionsTests
    {
        [Fact]
        public void Map_OnSome()
        {
            var expected = Maybe.Some(2);
            var result = Maybe.Some(1).Map(v => v + 1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Map_OnNone()
        {
            var result = Maybe.None<int>().Map(v => Fail.With(v));
            result.AssertIsNone();
        }

        [Fact]
        public void Filter_OnSome_ReturningFalse_IsNone()
        {
            var result = Maybe.Some(1).Filter(v => false);
            result.AssertIsNone();
        }

        [Fact]
        public void Filter_OnSome_ReturningTrue_HasSameValue()
        {
            var maybe = Maybe.Some(new object());
            var filtered = maybe.Filter(v => true);
            Assert.Same(maybe.OrFail(), filtered.OrFail());
        }

        [Fact]
        public void Filter_OnNone_ReturningFalse_IsNone()
        {
            var filtered = Maybe.None<object>().Filter(v => false);
            filtered.AssertIsNone();
        }

        [Fact]
        public void Filter_OnNone_ReturningTrue_IsNone()
        {
            var filtered = Maybe.None<object>().Filter(v => true);
            filtered.AssertIsNone();
        }
    }
}