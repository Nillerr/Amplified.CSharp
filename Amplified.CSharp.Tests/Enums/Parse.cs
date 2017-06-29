using Xunit;

namespace Amplified.CSharp
{
    public class Parse
    {
        private enum Values
        {
            One
        }
        
        [Fact]
        public void WithNull_ReturnsNone()
        {
            var result = Enums.Parse<Values>(null);
            result.MustBeNone();
        }
        
        [Fact]
        public void WithStringMatchingAValue_ReturnsSome()
        {
            var result = Enums.Parse<Values>(nameof(Values.One));
            var value = result.MustBeSome();
            Assert.Equal(Values.One, value);
        }
        
        [Fact]
        public void WithStringMatchingAValue_ReturnsNone()
        {
            var result = Enums.Parse<Values>("InvalidValue");
            result.MustBeNone();
        }
    }
}