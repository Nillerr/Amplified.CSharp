using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__Match__Action
    {
        [Fact]
        public void WithLambda()
        {
            var invocations = 0;
            
            var source = new None();
            source.Match(() => { invocations++; });
            
            Assert.Equal(1, invocations);
        }
        
        [Fact]
        public void WithMethodReference()
        {
            var invocations = 0;
            
            void Match() => invocations++;

            var source = new None();
            source.Match(Match);
            
            Assert.Equal(1, invocations);
        }
    }
}
