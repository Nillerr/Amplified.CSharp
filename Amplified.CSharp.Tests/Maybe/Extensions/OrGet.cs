using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__OrGet
    {
        [Fact]
        public void OnSome_ReturnsValue()
        {
            const int expected = 6;
            var source = Maybe<int>.Some(expected);
            var result = source.OrGet(() => 10);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void OnSome_DoesNotInvokeLambda()
        {
            var rec = new Recorder();
            var source = Maybe<int>.Some(6);
            source.OrGet(rec.Record(() => 10));
            rec.MustHaveExactly(0.Invocations());
        }

        [Fact]
        public void OnNone_ReturnsResultOfLambda()
        {
            const int expected = 10;
            var source = Maybe<int>.None();
            var result = source.OrGet(() => expected);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnNone_ReturnsResultOfMethodReference()
        {
            const int expected = 10;
            int Getter() => expected;
            
            var source = Maybe<int>.None();
            var result = source.OrGet(Getter);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnNone_InvokesLambdaOnce()
        {
            var rec = new Recorder();
            var source = Maybe<int>.None();
            source.OrGet(rec.Record(() => 10));
            rec.MustHaveExactly(1.Invocations());
        }
    }
}
