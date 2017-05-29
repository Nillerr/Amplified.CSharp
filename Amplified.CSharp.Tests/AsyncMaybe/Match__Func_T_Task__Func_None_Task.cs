using System.Threading.Tasks;
using Xunit;
using static Amplified.CSharp.Units;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Match__Func_T_Task__Func_None_Task
    {
        [Fact]
        public async Task WithLambdas()
        {
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(some => Task.CompletedTask, none => Task.CompletedTask);
            Assert.Equal(result, Unit());
        }
        
        [Fact]
        public async Task WithSomeLambda_AndNoneReference()
        {
            Task MatchNone(None none) => Task.CompletedTask;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: some => Task.CompletedTask, noneAsync: MatchNone);
            Assert.Equal(result, Unit());
        }
        
        [Fact]
        public async Task WithSomeReference_AndNoneLambda()
        {
            Task MatchSome(int some) => Task.CompletedTask;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: MatchSome, noneAsync: none => Task.CompletedTask);
            Assert.Equal(result, Unit());
        }
        
        [Fact]
        public async Task WithReferences()
        {
            Task MatchSome(int some) => Task.CompletedTask;
            Task MatchNone(None none) => Task.CompletedTask;
            
            var source = AsyncMaybe<int>.Some(1);
            var result = await source.Match(someAsync: MatchSome, noneAsync: MatchNone);
            Assert.Equal(result, Unit());
        }
    }
}
