using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__MatchNone__Action
    {
        [Fact]
        public void WhenNone_InvokesAction()
        {
            var invocations = 0;
            var source = Maybe<int>.None();
            var result = source.MatchNone(() => { invocations++; });
            Assert.IsType<Unit>(result);
            Assert.Equal(1, invocations);
        }
        
        [Fact]
        public void WhenSome_DoesNotInvokeAction()
        {
            var invocations = 0;
            var source = Maybe<int>.Some(23);
            var result = source.MatchNone(() => { invocations++; });
            Assert.IsType<Unit>(result);
            Assert.Equal(0, invocations);
        }
    }
}
