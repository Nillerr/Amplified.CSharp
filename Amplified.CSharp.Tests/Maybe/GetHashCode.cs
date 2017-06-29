using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__GetHashCode
    {
        [Fact]
        public void WhenSome_ReturnsSameHashCode_WithDifferentValues()
        {
            var value = new object();
            var source = Maybe<object>.Some(value);
            var other = Maybe<object>.Some(value);
            Assert.Equal(source.GetHashCode(), other.GetHashCode());
        }
        
        [Fact]
        public void WhenSome_ReturnsDifferentHashCode_WithDifferentValues()
        {
            var source = Maybe<object>.Some(new object());
            var other = Maybe<object>.Some(new object());
            Assert.NotEqual(source.GetHashCode(), other.GetHashCode());
        }
        
        [Fact]
        public void WhenNone_ReturnsZero()
        {
            var source = Maybe<object>.None();
            Assert.Equal(0, source.GetHashCode());
        }
        
        [Fact]
        public void WhenNone_ReturnsSameAsNone()
        {
            var source = Maybe<object>.None();
            var none = new None();
            Assert.Equal(none.GetHashCode(), source.GetHashCode());
        }
    }
}
