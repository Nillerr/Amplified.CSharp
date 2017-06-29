using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__op_Equality
    {
        [Fact]
        public void WithSome_AndOtherSome_WithSameValues()
        {
            var source = Maybe<int>.Some(1);
            var other = Maybe<int>.Some(1);

            var result = source == other;
            Assert.True(result);
        }
        
        [Fact]
        public void WithSome_AndOtherSome_WithDifferentValues()
        {
            var source = Maybe<int>.Some(1);
            var other = Maybe<int>.Some(2);

            var result = source == other;
            Assert.False(result);
        }
        
        [Fact]
        public void WithSome_AndOtherMaybeNone()
        {
            var source = Maybe<int>.Some(1);
            var other = Maybe<int>.None();

            var result = source == other;
            Assert.False(result);
        }
        
        [Fact]
        public void WithSome_AndOtherNone()
        {
            var source = Maybe<int>.Some(1);
            var other = new None();

            var result = source == other;
            Assert.False(result);
        }
    }
}
