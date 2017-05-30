using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Filter
    {
        [Fact]
        public void OnSome_WithTruePredicate_ReturnsSome()
        {
            var expected = new {Flag = true};
            var source = Maybe.Some(expected).Filter(it => it.Flag);
            source.MustBeSome();
        }

        [Fact]
        public void OnSome_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = false};
            var source = Maybe.Some(expected).Filter(it => it.Flag);
            source.MustBeNone();
        }
        
        [Fact]
        public void OnNone_WithTruePredicate_ReturnsNone()
        {
            var invocations = 0;
            var source = Maybe<object>.None().Filter(it => { invocations++; return true; });
            source.MustBeNone();
            Assert.Equal(0, invocations);
        }

        [Fact]
        public void OnNone_WithFalsePredicate_ReturnsNone()
        {
            var invocations = 0;
            var source = Maybe<object>.None().Filter(it => { invocations++; return true; });
            source.MustBeNone();
            Assert.Equal(0, invocations);
        }
    }
}
