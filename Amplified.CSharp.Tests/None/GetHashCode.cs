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
            Assert.Equal(hashCode, 0);
        }
    }
}
