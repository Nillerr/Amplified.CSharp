using Xunit;
using static Amplified.CSharp.Units;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__Match__Action_None
    {
        [Fact]
        public void WithLambda()
        {
            var source = new None();
            var result = source.Match(none => { });
            Assert.Equal(Unit(), result);
        }
        
        [Fact]
        public void WithMethodReference()
        {
            void Match(None none)
            {
            }

            var source = new None();
            var result = source.Match(Match);
            Assert.Equal(Unit(), result);
        }
    }
}
