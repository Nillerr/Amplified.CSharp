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
            var some = 0;
            var none = 0;
            
            var source = Some(1);
            source.Match(_ => { some++; }, _ => { none++; });

            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
        
        [Fact]
        public void WithSomeLambda_AndNoneReference()
        {
            var some = 0;
            var none = 0;

            void MatchNone(None _) => none++;
            
            var source = Some(1);
            source.Match(_ => { some++; }, MatchNone);
            
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
        
        [Fact]
        public void WithSomeReference_AndNoneLambda()
        {
            var some = 0;
            var none = 0;

            void MatchSome(int _) => some++;
            
            var source = Some(1);
            source.Match(MatchSome, () => { none++; });
            
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
        
        [Fact]
        public void WithReferences()
        {
            var some = 0;
            var none = 0;

            void MatchSome(int _) => some++;
            void MatchNone(None _) => none++;
            
            var source = Some(1);
            source.Match(MatchSome, MatchNone);
            
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
    }
}
