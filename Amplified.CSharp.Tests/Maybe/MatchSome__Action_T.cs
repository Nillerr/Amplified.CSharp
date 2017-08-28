using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__MatchSome__Action_T
    {
        [Fact]
        public void WhenSome_InvokesAction()
        {
            var invocations = 0;
            var source = Maybe<int>.Some(23);
            source.MatchSome(value => { invocations++; });
            Assert.Equal(1, invocations);
        }
        
        [Fact]
        public void WhenNone_DoesNotInvokeAction()
        {
            var invocations = 0;
            var source = Maybe<int>.None();
            source.MatchSome(value => { invocations++; });
            Assert.Equal(0, invocations);
        }
    }
}
