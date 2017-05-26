using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    public class MaybeEqualityTests
    {
        [Fact]
        public void MaybeSome_IsNotEqualTo_SimpleDefaultMaybe()
        {
            var explicitSome = Some(0);
            var implicitNone = default(Maybe<int>);
            Assert.NotEqual(explicitSome, implicitNone);
            Assert.False(explicitSome == implicitNone);
            Assert.True(explicitSome != implicitNone);
        }

        [Fact]
        public void MaybeNone_IsEqualTo_None()
        {
            var maybe = Maybe<int>.None;
            var none = default(None);
            Assert.Equal(maybe, none);
            Assert.True(maybe == none);
            Assert.False(maybe != none);
            Assert.True(maybe.Equals(none));
        }

        [Fact]
        public void MaybeSome_IsNotEqualTo_None()
        {
            var maybe = Some(2);
            var none = default(None);
            Assert.NotEqual(maybe, none);
            Assert.False(maybe == none);
            Assert.True(maybe != none);
            Assert.False(maybe.Equals(none));
        }

        [Fact]
        public void None_IsEqualTo_None()
        {
            var lhs = Maybe<int>.None;
            var rhs = Maybe<int>.None;
            Assert.Equal(lhs, rhs);
            Assert.True(lhs == rhs);
            Assert.False(lhs != rhs);
            Assert.True(lhs.Equals(rhs));
        }

        [Fact]
        public void Some_IsEqualTo_Some_WithSameValue()
        {
            var lhs = Some(1);
            var rhs = Some(1);
            Assert.Equal(lhs, rhs);
            Assert.True(lhs == rhs);
            Assert.False(lhs != rhs);
            Assert.True(lhs.Equals(rhs));
        }

        [Fact]
        public void Some_IsNotEqualTo_Some_WithDifferentValue()
        {
            var lhs = Some(1);
            var rhs = Some(2);
            Assert.NotEqual(lhs, rhs);
            Assert.False(lhs == rhs);
            Assert.True(lhs != rhs);
            Assert.False(lhs.Equals(rhs));
        }

        [Fact]
        public void Some_IsNotEqualTo_None()
        {
            var some = Some(1);
            var none = Maybe<int>.None;
            Assert.NotEqual(some, none);
            Assert.False(some == none);
            Assert.True(some != none);
            Assert.False(some.Equals(none));
        }
    }
}