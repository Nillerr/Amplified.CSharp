using System.Threading.Tasks;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Match__Func_T_Task__Func_None_Task
    {
        [Fact]
        public async Task NoneValue_WithLambdas()
        {
            var some = 0;
            var none = 0;
            var source = AsyncMaybe<int>.None();
            await source.Match(_ => { some++; return Task.CompletedTask; }, _ => { none++; return Task.CompletedTask; });
            Assert.Equal(0, some);
            Assert.Equal(1, none);
        }
        
        [Fact]
        public async Task WithLambdas()
        {
            var some = 0;
            var none = 0;
            var source = AsyncMaybe<int>.Some(1);
            await source.Match(_ => { some++; return Task.CompletedTask; }, _ => { none++; return Task.CompletedTask; });
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
        
        [Fact]
        public async Task WithSomeLambda_AndNoneReference()
        {
            var some = 0;
            var none = 0;
            
            Task MatchNone(None _) { none++; return Task.CompletedTask; }
            
            var source = AsyncMaybe<int>.Some(1);
            await source.Match(someAsync: _ => { some++; return Task.CompletedTask; }, noneAsync: MatchNone);
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
        
        [Fact]
        public async Task WithSomeReference_AndNoneLambda()
        {
            var some = 0;
            var none = 0;
            
            Task MatchSome(int _) { some++; return Task.CompletedTask; }
            
            var source = AsyncMaybe<int>.Some(1);
            await source.Match(someAsync: MatchSome, noneAsync: _ => { none++; return Task.CompletedTask; });
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
        
        [Fact]
        public async Task WithReferences()
        {
            var some = 0;
            var none = 0;
            
            Task MatchSome(int _) { some++; return Task.CompletedTask; }
            Task MatchNone(None _) { none++; return Task.CompletedTask; }
            
            var source = AsyncMaybe<int>.Some(1);
            await source.Match(someAsync: MatchSome, noneAsync: MatchNone);
            Assert.Equal(1, some);
            Assert.Equal(0, none);
        }
    }
}
