using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__FlatMap
    {
        [Fact]
        public void OnSome_WithInnerSome_ReturnsSome()
        {
            const double expected = 6.0;
            var source = Maybe<int>.Some(1).FlatMap(it => Maybe<double>.Some(expected));
            var result = source.OrFail();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void OnSome_WithInnerNone_ReturnsNone()
        {
            var source = Maybe<int>.Some(1).FlatMap(it => Maybe<double>.None());
            source.MustBeNone();
        }
        
        [Fact]
        public void OnSome_InvokesFuncOnce()
        {
            var rec = new Recorder();
            Maybe<int>.Some(1).FlatMap(rec.Record((int it) => Maybe<double>.None())).OrDefault();
            rec.MustHaveExactly(1.Invocations());
        }

        [Fact]
        public void OnNone_WithInnerSome_ReturnsNone()
        {
            var source = Maybe<int>.None().FlatMap(it => Maybe<double>.Some(7.0));
            source.MustBeNone();
        }

        [Fact]
        public void OnNone_WithInnerNone_ReturnsNone()
        {
            var source = Maybe<int>.None().FlatMap(it => Maybe<double>.None());
            source.MustBeNone();
        }

        [Fact]
        public void OnNone_DoesNotInvokeFunc()
        {
            var rec = new Recorder();
            Maybe<int>.None().FlatMap(rec.Record((int it) => Maybe<double>.Some(7.0))).OrDefault();
            rec.MustHaveExactly(0.Invocations());
        }
    }
}
