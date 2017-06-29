using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__OrDefault
    {
        [Fact]
        public void OnSome_ReturnsValue()
        {
            const int expected = 6;
            var source = Maybe<int>.Some(expected);
            var result = source.OrDefault();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void OnNone_WithValueType_ReturnsDefaultValue()
        {
            var source = Maybe<int>.None();
            var result = source.OrDefault();
            Assert.Equal(default(int), result);
        }
        
        [Fact]
        public void OnNone_WithReferenceType_ReturnsDefaultValue()
        {
            var source = Maybe<object>.None();
            var result = source.OrDefault();
            Assert.Equal(default(object), result);
        }
    }
}
