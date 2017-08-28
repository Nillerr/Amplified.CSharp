using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__Match__Action_None
    {
        [Fact]
        public void WithLambda()
        {
            var invocations = 0;
            
            var source = new None();
            source.Match(none => { invocations++; });
            
            Assert.Equal(1, invocations);
        }
        
        [Fact]
        public void WithMethodReference()
        {
            var invocations = 0;
            
            void Match(None none) => invocations++;

            var source = new None();
            source.Match(Match);
            
            Assert.Equal(1, invocations);
        }
    }
}
