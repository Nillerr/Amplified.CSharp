using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Match__Func_T_TResult__Func_TResult
    {
        [Fact]
        public void WithLambdas()
        {
            var source = Some(1);
            var result = source.Match(some => some + 1, () => 0);
            Assert.Equal(2, result);
        }
        
        [Fact]
        public void WithSomeLambda_AndNoneReference()
        {
            int MatchNone() => 0;
            
            var source = Some(1);
            var result = source.Match(some => some + 1, MatchNone);
            Assert.Equal(2, result);
        }
        
        [Fact]
        public void WithSomeReference_AndNoneLambda()
        {
            int MatchSome(int some) => some + 1;
            
            var source = Some(1);
            var result = source.Match(MatchSome, () => 0);
            Assert.Equal(2, result);
        }
        
        [Fact]
        public void WithReferences()
        {
            int MatchSome(int some) => some + 1;
            int MatchNone() => 0;
            
            var source = Some(1);
            var result = source.Match(MatchSome, MatchNone);
            Assert.Equal(2, result);
        }
    }
}
