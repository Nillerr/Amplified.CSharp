using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Unit__ToAny
    {
        [Fact]
        public void Return()
        {
            var source = default(Unit);
            var result = source.Return(23);
            Assert.Equal(23, result);
        }

        [Fact]
        public void Get()
        {
            var source = default(Unit);
            var result = source.Get(() => 23);
            Assert.Equal(23, result);
        }
    }
}