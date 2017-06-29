using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__NullableToMaybe
    {
        [Fact]
        public void WhenNull_ReturnsNone()
        {
            var source = (int?) null;
            
            var result = source.ToMaybe();
            Assert.IsType<Maybe<int>>(result);
            
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenNonNull_ReturnsSome()
        {
            var source = (int?) 5191;
            
            var result = source.ToMaybe();
            Assert.IsType<Maybe<int>>(result);
            
            var value = result.MustBeSome();
            Assert.Equal(5191, value);
        }
    }
}