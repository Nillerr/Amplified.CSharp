using Amplified.CSharp.Internal;
using Xunit;

namespace Amplified.CSharp
{
    public class MaybeEqualityTests
    {
        [Fact]
        public void MaybeSome_IsEqualTo_Some()
        {
            var maybe = Maybe.Some(2);
            var some = Some._(2);
            Assert.Equal(maybe, some);
            Assert.True(maybe == some);
            Assert.False(maybe != some);
        }

        [Fact]
        public void MaybeSome_IsNotEqualTo_SimpleDefaultMaybe()
        {
            var explicitSome = Maybe.Some(0);
            var implicitNone = default(Maybe<int>);
            Assert.NotEqual(explicitSome, implicitNone);
            Assert.False(explicitSome == implicitNone);
            Assert.True(explicitSome != implicitNone);
        }

        [Fact]
        public void MaybeNone_IsEqualTo_None()
        {
            var maybe = Maybe<int>.None;
            var none = None._;
            Assert.Equal(maybe, none);
            Assert.True(maybe == none);
            Assert.False(maybe != none);
        }

        [Fact]
        public void None_IsEqualTo_None()
        {
            var lhs = Maybe<int>.None;
            var rhs = Maybe<int>.None;
            Assert.Equal(lhs, rhs);
            Assert.True(lhs == rhs);
            Assert.False(lhs != rhs);
        }

        [Fact]
        public void Some_IsEqualTo_Some_WithSameValue()
        {
            var lhs = Maybe.Some(1);
            var rhs = Maybe.Some(1);
            Assert.Equal(lhs, rhs);
            Assert.True(lhs == rhs);
            Assert.False(lhs != rhs);
        }

        [Fact]
        public void Some_IsNotEqualTo_Some_WithDifferentValue()
        {
            var lhs = Maybe.Some(1);
            var rhs = Maybe.Some(2);
            Assert.NotEqual(lhs, rhs);
            Assert.False(lhs == rhs);
            Assert.True(lhs != rhs);
        }

        [Fact]
        public void Some_IsNotEqualTo_None()
        {
            var some = Maybe.Some(1);
            var none = Maybe<int>.None;
            Assert.NotEqual(some, none);
            Assert.False(some == none);
            Assert.True(some != none);
        }
    }
}