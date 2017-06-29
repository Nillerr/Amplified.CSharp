using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__GetHashCode
    {
        [Fact]
        public void ReturnsZero()
        {
            var source = new None();
            var hashCode = source.GetHashCode();
            Assert.Equal(0, hashCode);
        }
        
        [Fact]
        public void IsEqualToHashCodeOfOtherNone()
        {
            var source = new None();
            var other = new None();
            
            var sourceHashCode = source.GetHashCode();
            var otherHashCode = other.GetHashCode();
            Assert.Equal(sourceHashCode, otherHashCode);
        }
    }
}
