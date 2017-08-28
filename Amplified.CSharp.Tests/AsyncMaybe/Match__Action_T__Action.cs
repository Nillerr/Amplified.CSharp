using System.Threading.Tasks;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Match__Action_T__Action
    {
        [Fact]
        public async Task NoneValue_WithLambdas()
        {
            var some = 0;
            var none = 0;
            var source = AsyncMaybe<int>.None();
            await source.Match(value => { some++; }, none: () => { none++; });
            Assert.Equal(0, some);
            Assert.Equal(1, none);
        }
        
        [Fact]
        public async Task WithLambdas()
        {
            var some = 0;
            var none = 0;
            var source = AsyncMaybe<int>.Some(1);
            await source.Match(value => { some++; }, none: () => { none++; });
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
        
        [Fact]
        public async Task WithSomeLambda_AndNoneReference()
        {
            var some = 0;
            var none = 0;
            
            void MatchNone() => none++;
            
            var source = AsyncMaybe<int>.Some(1);
            await source.Match(value => { some++; }, none: MatchNone);
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
        
        [Fact]
        public async Task WithSomeReference_AndNoneLambda()
        {
            var some = 0;
            var none = 0;
            
            void MatchSome(int value) => some++;
            
            var source = AsyncMaybe<int>.Some(1);
            await source.Match(some: MatchSome, none: () => { none++; });
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
        
        [Fact]
        public async Task WithReferences()
        {
            var some = 0;
            var none = 0;
            
            void MatchSome(int value) => some++;
            void MatchNone() => none++;

            var source = AsyncMaybe<int>.Some(1);
            await source.Match(some: MatchSome, none: MatchNone);
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
    }
}
