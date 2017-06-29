using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Unit__Match
    {
        [Fact]
        public void WithValue()
        {
            var source = new Unit();
            var result = source.Match(5);
            Assert.Equal(5, result);
        }
        
        [Fact]
        public void WithNoArgFunc()
        {
            var source = new Unit();
            var result = source.Match(() => 5);
            Assert.Equal(5, result);
        }
        
        [Fact]
        public void WithUnitArgFunc()
        {
            var source = new Unit();
            var result = source.Match(unit => 5);
            Assert.Equal(5, result);
        }
    }
}
