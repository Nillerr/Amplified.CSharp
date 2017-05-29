using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Match__Func_T_TResult__Func_TResult
    {
        [Fact]
        public void WithLambdas()
        {
            var source = Some(1);
            var result = source.Match(some => some + 1, () => 0);
            Assert.Equal(result, 2);
        }
        
        [Fact]
        public void WithSomeLambda_AndNoneReference()
        {
            int MatchNone() => 0;
            
            var source = Some(1);
            var result = source.Match(some => some + 1, MatchNone);
            Assert.Equal(result, 2);
        }
        
        [Fact]
        public void WithSomeReference_AndNoneLambda()
        {
            int MatchSome(int some) => some + 1;
            
            var source = Some(1);
            var result = source.Match(MatchSome, () => 0);
            Assert.Equal(result, 2);
        }
        
        [Fact]
        public void WithReferences()
        {
            int MatchSome(int some) => some + 1;
            int MatchNone() => 0;
            
            var source = Some(1);
            var result = source.Match(MatchSome, MatchNone);
            Assert.Equal(result, 2);
        }
    }
}
