using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__FlatMapAsync
    {
        [Fact]
        public async Task OnSome_WithInnerSome_ReturnsSome()
        {
            const double expected = 6.0;
            var source = await Maybe<int>.Some(1).FlatMapAsync(it => Task.FromResult(Maybe<double>.Some(expected)));
            var result = source.OrFail();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task OnSome_WithInnerNone_ReturnsNone()
        {
            var source = await Maybe<int>.Some(1).FlatMapAsync(it => Task.FromResult(Maybe<double>.None()));
            source.MustBeNone();
        }
        
        [Fact]
        public async Task OnSome_InvokesFuncOnce()
        {
            var rec = new Recorder();
            await Maybe<int>.Some(1).FlatMapAsync(rec.Record((int it) => Task.FromResult(Maybe<double>.None()))).OrDefault();
            rec.MustHaveExactly(1.Invocations());
        }

        [Fact]
        public async Task OnNone_WithInnerSome_ReturnsNone()
        {
            var source = await Maybe<int>.None().FlatMapAsync(it => Task.FromResult(Maybe<double>.Some(7.0)));
            source.MustBeNone();
        }

        [Fact]
        public async Task OnNone_WithInnerNone_ReturnsNone()
        {
            var source = await Maybe<int>.None().FlatMapAsync(it => Task.FromResult(Maybe<double>.None()));
            source.MustBeNone();
        }

        [Fact]
        public async Task OnNone_DoesNotInvokeFunc()
        {
            var rec = new Recorder();
            await Maybe<int>.None().FlatMapAsync(rec.Record((int it) => Task.FromResult(Maybe<double>.Some(7.0)))).OrDefault();
            rec.MustHaveExactly(0.Invocations());
        }
    }
}
