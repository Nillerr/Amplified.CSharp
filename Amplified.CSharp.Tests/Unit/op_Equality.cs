using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Unit__op_Equality
    {
        [Fact]
        public void WithUnit()
        {
            var unit = new Unit();
            var other = new Unit();
            Assert.True(unit == other);
        }
    }
}
