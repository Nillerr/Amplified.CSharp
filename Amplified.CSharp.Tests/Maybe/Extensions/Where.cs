using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Where
    {
        [Fact]
        public void OnSome_WithTruePredicate_ReturnsSameAsFilter()
        {
            const int value = 3;
            var source = Maybe<int>.Some(value);
            
            var whered = source.Where(v => true);
            var filtered = source.Filter(v => true);

            Assert.Equal(whered, filtered);
        }
        
        [Fact]
        public void OnSome_WithFalsePredicate_ReturnsSameAsFilter()
        {
            const int value = 3;
            var source = Maybe<int>.Some(value);
            
            var whered = source.Where(v => false);
            var filtered = source.Filter(v => false);

            Assert.Equal(whered, filtered);
        }
        
        [Fact]
        public void OnNone_WithTruePredicate_ReturnsSameAsFilter()
        {
            var source = Maybe<int>.None();
            
            var whered = source.Where(v => true);
            var filtered = source.Filter(v => true);

            Assert.Equal(whered, filtered);
        }
        
        [Fact]
        public void OnNone_WithFalsePredicate_ReturnsSameAsFilter()
        {
            var source = Maybe<int>.None();
            
            var whered = source.Where(v => false);
            var filtered = source.Filter(v => false);

            Assert.Equal(whered, filtered);
        }
    }
}
