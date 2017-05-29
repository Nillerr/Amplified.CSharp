using System.Threading.Tasks;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Match__Func_T_TResult__Func_None_TResult
    {
        [Fact]
        public async Task WithLambdas()
        {
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some => some + 1, none => 0);
            Assert.Equal(2, result);
        }
        
        [Fact]
        public async Task WithSomeLambda_AndNoneReference()
        {
            int MatchNone(None none) => 0;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some => some + 1, none: MatchNone);
            Assert.Equal(2, result);
        }
        
        [Fact]
        public async Task WithSomeReference_AndNoneLambda()
        {
            int MatchSome(int some) => some + 1;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some: MatchSome, none: none => 0);
            Assert.Equal(2, result);
        }
        
        [Fact]
        public async Task WithReferences()
        {
            int MatchSome(int some) => some + 1;
            int MatchNone(None none) => 0;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some: MatchSome, none: MatchNone);
            Assert.Equal(2, result);
        }
    }
}
