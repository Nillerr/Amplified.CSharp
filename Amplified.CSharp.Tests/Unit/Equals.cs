using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Unit__Equals
    {
        [Fact]
        public void WithUnit()
        {
            var unit = new Unit();
            var other = new Unit();
            Assert.True(unit.Equals(other));
            Assert.True(other.Equals(unit));
            Assert.True(Equals(unit, other));
            Assert.Equal(unit, other);
        }
        
        [Fact]
        public void WithUnitObject()
        {
            var unit = new Unit();
            var other = (object) new Unit();
            Assert.True(unit.Equals(other));
            Assert.True(other.Equals(unit));
            Assert.True(Equals(unit, other));
            Assert.Equal(unit, other);
        }
        
        [Fact]
        public void WithObjectNull()
        {
            var unit = new Unit();
            var other = (object) null;
            Assert.False(unit.Equals(other));
            Assert.NotEqual(unit, other);
            Assert.False(Equals(unit, other));
        }
    }
}
