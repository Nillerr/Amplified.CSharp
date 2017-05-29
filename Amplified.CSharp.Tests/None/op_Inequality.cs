using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__op_Inequality
    {
        [Fact]
        public void NotEqualsOtherNone()
        {
            var source = new None();
            var other = new None();
            var result = source != other;
            Assert.False(result);
        }
    }
}
