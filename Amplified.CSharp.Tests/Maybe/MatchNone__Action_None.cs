using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__MatchNone__Action_None
    {
        [Fact]
        public void WhenNone_InvokesAction()
        {
            var invocations = 0;
            var source = Maybe<int>.None();
            source.MatchNone(none => { invocations++; });
            Assert.Equal(1, invocations);
        }
        
        [Fact]
        public void WhenSome_DoesNotInvokeAction()
        {
            var invocations = 0;
            var source = Maybe<int>.Some(23);
            source.MatchNone(none => { invocations++; });
            Assert.Equal(0, invocations);
        }
    }
}
