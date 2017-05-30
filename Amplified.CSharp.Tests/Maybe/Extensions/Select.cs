using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Select
    {
        [Fact]
        public void OnSome_ReturnsSameAsMap()
        {
            const int value = 3;
            var source = Maybe<int>.Some(value);
            
            var whered = source.Select(v => v + 1);
            var filtered = source.Map(v => v + 1);

            Assert.Equal(whered, filtered);
        }
        
        [Fact]
        public void OnNone_ReturnsSameAsMap()
        {
            var source = Maybe<int>.None();
            
            var whered = source.Select(v => true);
            var filtered = source.Map(v => true);

            Assert.Equal(whered, filtered);
        }
    }
}
