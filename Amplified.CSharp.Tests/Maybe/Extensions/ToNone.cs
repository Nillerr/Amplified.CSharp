using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__ToNone
    {
        [Fact]
        public void WhenSome_ReturnsNone()
        {
            var source = Maybe<int>.Some(23);
            var result = source.ToNone();
            Assert.Equal(default(None), result);
        }
        
        [Fact]
        public void WhenNone_ReturnsNone()
        {
            var source = Maybe<int>.None();
            var result = source.ToNone();
            Assert.Equal(default(None), result);
        }
    }
}