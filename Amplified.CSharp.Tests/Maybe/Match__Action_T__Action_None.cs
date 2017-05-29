using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Match__Action_T__Action_None
    {
        [Fact]
        public void WithLambdas()
        {
            var source = Some(1);
            var result = source.Match(some => { }, none => { });
            Assert.Equal(result, Units.Unit());
        }
        
        [Fact]
        public void WithSomeLambda_AndNoneReference()
        {
            void MatchNone(None none)
            {
            }
            
            var source = Some(1);
            var result = source.Match(some => { }, MatchNone);
            Assert.Equal(result, Units.Unit());
        }
        
        [Fact]
        public void WithSomeReference_AndNoneLambda()
        {
            void MatchSome(int some)
            {
            }
            
            var source = Some(1);
            var result = source.Match(MatchSome, () => { });
            Assert.Equal(result, Units.Unit());
        }
        
        [Fact]
        public void WithReferences()
        {
            void MatchSome(int some)
            {
            }

            void MatchNone(None none)
            {
            }
            
            var source = Some(1);
            var result = source.Match(MatchSome, MatchNone);
            Assert.Equal(result, Units.Unit());
        }
    }
}
