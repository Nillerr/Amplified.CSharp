using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__OrGetAsync
    {
        [Fact]
        public async Task OnSome_ReturnsValue()
        {
            const int expected = 6;
            var source = Maybe<int>.Some(expected);
            var result = await source.OrGetAsync(async () => 10);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task OnSome_DoesNotInvokeLambda()
        {
            var rec = new Recorder();
            var source = Maybe<int>.Some(6);
            await source.OrGetAsync(rec.Record(async () => 10));
            rec.MustHaveExactly(0.Invocations());
        }

        [Fact]
        public async Task OnNone_ReturnsResultOfLambda()
        {
            const int expected = 10;
            var source = Maybe<int>.None();
            var result = await source.OrGetAsync(async () => expected);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task OnNone_ReturnsResultOfMethodReference()
        {
            const int expected = 10;
            async Task<int> Getter() => expected;
            
            var source = Maybe<int>.None();
            var result = await source.OrGetAsync(Getter);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task OnNone_InvokesLambdaOnce()
        {
            var rec = new Recorder();
            var source = Maybe<int>.None();
            await source.OrGetAsync(rec.Record(async () => 10));
            rec.MustHaveExactly(1.Invocations());
        }
    }
}