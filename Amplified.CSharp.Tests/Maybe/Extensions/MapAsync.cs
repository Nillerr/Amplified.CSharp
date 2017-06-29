using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__MapAsync
    {
        [Fact]
        public async Task OnSome_ReturnsSome_WithMappedResult()
        {
            const long expected = 23000;
            var source = await Maybe<int>.Some(1).MapAsync(it => Task.FromResult(expected));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task OnNone_ReturnsNone()
        {
            Maybe<double> source = await Maybe<int>.None().MapAsync(it => Task.FromResult<double>(690343));
            source.MustBeNone();
        }
    }
}
