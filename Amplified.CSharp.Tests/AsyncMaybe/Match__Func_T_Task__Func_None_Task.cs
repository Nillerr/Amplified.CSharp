using System.Threading.Tasks;
using Xunit;
using static Amplified.CSharp.Units;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Match__Func_T_Task__Func_None_Task
    {
        [Fact]
        public async Task NoneValue_WithLambdas()
        {
            var invocations = 0;
            var source = AsyncMaybe<int>.None();
            var result = await source.Match(some => TaskCache.CompletedTask, none => { invocations++; return TaskCache.CompletedTask; });
            Assert.Equal(Unit(), result);
            Assert.Equal(1, invocations);
        }
        
        [Fact]
        public async Task WithLambdas()
        {
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some => TaskCache.CompletedTask, none => TaskCache.CompletedTask);
            Assert.Equal(Unit(), result);
        }
        
        [Fact]
        public async Task WithSomeLambda_AndNoneReference()
        {
            Task MatchNone(None none) => TaskCache.CompletedTask;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: some => TaskCache.CompletedTask, noneAsync: MatchNone);
            Assert.Equal(Unit(), result);
        }
        
        [Fact]
        public async Task WithSomeReference_AndNoneLambda()
        {
            Task MatchSome(int some) => TaskCache.CompletedTask;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: MatchSome, noneAsync: none => TaskCache.CompletedTask);
            Assert.Equal(Unit(), result);
        }
        
        [Fact]
        public async Task WithReferences()
        {
            Task MatchSome(int some) => TaskCache.CompletedTask;
            Task MatchNone(None none) => TaskCache.CompletedTask;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: MatchSome, noneAsync: MatchNone);
            Assert.Equal(Unit(), result);
        }
    }
}
