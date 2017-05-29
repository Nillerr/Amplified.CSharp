using System.Threading.Tasks;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Match__Func_T_Task_TResult__Func_None_TResult
    {
        [Fact]
        public async Task NoneValue_WithLambdas()
        {
            var invocations = 0;
            var source = AsyncMaybe<int>.None();
            var result = await source.Match(some => Task.FromResult(some + 1), none => { invocations++; return 0; });
            Assert.Equal(0, result);
            Assert.Equal(1, invocations);
        }
        
        [Fact]
        public async Task WithLambdas()
        {
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some => Task.FromResult(some + 1), none => 0);
            Assert.Equal(2, result);
        }
        
        [Fact]
        public async Task WithSomeLambda_AndNoneReference()
        {
            int MatchNone(None none) => 0;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some => Task.FromResult(some + 1), none: MatchNone);
            Assert.Equal(2, result);
        }
        
        [Fact]
        public async Task WithSomeReference_AndNoneLambda()
        {
            Task<int> MatchSome(int some) => Task.FromResult(some + 1);
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: MatchSome, none: none => 0);
            Assert.Equal(2, result);
        }
        
        [Fact]
        public async Task WithReferences()
        {
            Task<int> MatchSome(int some) => Task.FromResult(some + 1);
            int MatchNone(None none) => 0;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: MatchSome, none: MatchNone);
            Assert.Equal(2, result);
        }
    }
}
