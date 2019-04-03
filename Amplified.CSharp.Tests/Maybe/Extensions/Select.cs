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
            
            var selected = source.Select(v => v + 1);
            var mapped = source.Map(v => v + 1);

            Assert.Equal(selected, mapped);
        }
        
        [Fact]
        public void OnNone_ReturnsSameAsMap()
        {
            var source = Maybe<int>.None();
            
            var selected = source.Select(v => true);
            var mapped = source.Map(v => true);

            Assert.Equal(selected, mapped);
        }
    }
}
