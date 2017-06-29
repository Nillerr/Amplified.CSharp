using System.Threading.Tasks;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__op_Implicit
    {
        [Fact]
        public async Task FromMaybeNone()
        {
            Maybe<object> source = Maybe<object>.None();
            AsyncMaybe<object> implicitAsync = source;
            
            var isNone = await implicitAsync.IsNone;
            Assert.True(isNone);
        }

        [Fact]
        public async Task FromMaybeSome()
        {
            var expectedValue = new object();
            
            Maybe<object> source = Maybe<object>.Some(expectedValue);
            AsyncMaybe<object> implicitAsync = source;

            var isSome = await implicitAsync.IsSome;
            var value = await implicitAsync.OrFail();
            Assert.True(isSome);
            Assert.Same(expectedValue, value);
        }

        [Fact]
        public async Task FromNone()
        {
            None source = new None();
            AsyncMaybe<object> implicitAsync = source;
            
            var isNone = await implicitAsync.IsNone;
            Assert.True(isNone);
        }
    }
}
