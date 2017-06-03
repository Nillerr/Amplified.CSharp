using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Any__ToUnit
    {
        [Fact]
        public void ToUnit()
        {
            var source = 23;
            var result = source.ToUnit();
            Assert.IsType<Unit>(result);
            Assert.Equal(default(Unit), result);
        }
    }
}