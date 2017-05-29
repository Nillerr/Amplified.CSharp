using System.Threading.Tasks;
using Xunit;
using static Amplified.CSharp.Units;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Match__Action_T__Action_None
    {
        [Fact]
        public async Task WithLambdas()
        {
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some => { }, none => { });
            Assert.Equal(result, Unit());
        }
        
        [Fact]
        public async Task WithSomeLambda_AndNoneReference()
        {
            void MatchNone(None none)
            {
            }
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some => { }, none: MatchNone);
            Assert.Equal(result, Unit());
        }
        
        [Fact]
        public async Task WithSomeReference_AndNoneLambda()
        {
            void MatchSome(int some)
            {
            }
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some: MatchSome, none: () => { });
            Assert.Equal(result, Unit());
        }
        
        [Fact]
        public async Task WithReferences()
        {
            void MatchSome(int some)
            {
            }

            void MatchNone(None none)
            {
            }
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some: MatchSome, none: MatchNone);
            Assert.Equal(result, Unit());
        }
    }
}
