using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Unit__op_Inequality
    {
        [Fact]
        public void WithUnit()
        {
            var unit = new Unit();
            var other = new Unit();
            Assert.False(unit != other);
        }
    }
}
