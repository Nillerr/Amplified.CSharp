using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__Match__Func_None_TResult
    {
        [Fact]
        public void WithLambda()
        {
            var source = new None();
            var result = source.Match(none => 1);
            Assert.Equal(result, 1);
        }
        
        [Fact]
        public void WithMethodReference()
        {
            int Match(None none) => 1;
            
            var source = new None();
            var result = source.Match(Match);
            Assert.Equal(result, 1);
        }
    }
}
