using System.Threading.Tasks;
using Xunit;
using static Amplified.CSharp.Units;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Match__Func_T_Task__Func_Task
    {
        [Fact]
        public async Task NoneValue_WithLambdas()
        {
            var invocations = 0;
            var source = AsyncMaybe<int>.None();
            var result = await source.Match(some => Task.CompletedTask, () => { invocations++; return Task.CompletedTask; });
            Assert.Equal(Unit(), result);
            Assert.Equal(1, invocations);
        }
        
        [Fact]
        public async Task WithLambdas()
        {
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some => Task.CompletedTask, () => Task.CompletedTask);
            Assert.Equal(Unit(), result);
        }
        
        [Fact]
        public async Task WithSomeLambda_AndNoneReference()
        {
            Task MatchNone() => Task.CompletedTask;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: some => Task.CompletedTask, noneAsync: MatchNone);
            Assert.Equal(Unit(), result);
        }
        
        [Fact]
        public async Task WithSomeReference_AndNoneLambda()
        {
            Task MatchSome(int some) => Task.CompletedTask;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: MatchSome, noneAsync: () => Task.CompletedTask);
            Assert.Equal(Unit(), result);
        }
        
        [Fact]
        public async Task WithReferences()
        {
            Task MatchSome(int some) => Task.CompletedTask;
            Task MatchNone() => Task.CompletedTask;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: MatchSome, noneAsync: MatchNone);
            Assert.Equal(Unit(), result);
        }
    }
}
