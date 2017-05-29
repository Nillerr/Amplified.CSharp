using Xunit;
using static Amplified.CSharp.Maybe;
using static Amplified.CSharp.Units;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Match__Action_T__Action
    {
        [Fact]
        public void WithLambdas()
        {
            var source = Some(1);
            var result = source.Match(some => { }, () => { });
            Assert.Equal(result, Unit());
        }
        
        [Fact]
        public void WithSomeLambda_AndNoneReference()
        {
            void MatchNone()
            {
            }
            
            var source = Some(1);
            var result = source.Match(some => { }, MatchNone);
            Assert.Equal(result, Unit());
        }
        
        [Fact]
        public void WithSomeReference_AndNoneLambda()
        {
            void MatchSome(int some)
            {
            }
            
            var source = Some(1);
            var result = source.Match(MatchSome, () => { });
            Assert.Equal(result, Unit());
        }
        
        [Fact]
        public void WithReferences()
        {
            void MatchSome(int some)
            {
            }

            void MatchNone()
            {
            }
            
            var source = Some(1);
            var result = source.Match(MatchSome, MatchNone);
            Assert.Equal(result, Unit());
        }
    }
}
