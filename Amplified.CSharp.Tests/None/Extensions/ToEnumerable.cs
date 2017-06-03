using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__ToEnumerable
    {
        [Fact]
        public void ReturnsEmptyEnumerable()
        {
            var source = default(None);
            var result = source.ToEnumerable<int>();
            Assert.Empty(result);
        }
    }
}