using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Unit__GetHashCode
    {
        [Fact]
        public void Default_ReturnsValueEqualToNameOf()
        {
            var expected = nameof(Unit).GetHashCode();
            var unit = default(Unit);
            Assert.Equal(expected, unit.GetHashCode());
        }
        
        [Fact]
        public void StaticInstant_ReturnsValueEqualToNameOf()
        {
            var expected = nameof(Unit).GetHashCode();
            var unit = Unit.Instance;
            Assert.Equal(expected, unit.GetHashCode());
        }
        
        [Fact]
        public void Constructed_ReturnsValueEqualToNameOf()
        {
            var expected = nameof(Unit).GetHashCode();
            var unit = new Unit();
            Assert.Equal(expected, unit.GetHashCode());
        }
        
        [Fact]
        public void ReturnsSameValue()
        {
            Assert.Equal(default(Unit).GetHashCode(), new Unit().GetHashCode());
            Assert.Equal(default(Unit).GetHashCode(), Unit.Instance.GetHashCode());
        }
    }
}
