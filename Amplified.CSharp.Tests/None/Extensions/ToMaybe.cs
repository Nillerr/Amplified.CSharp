using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__ToMaybe
    {
        [Fact]
        public void ToMaybe_ReturnsNone()
        {
            var source = default(None);
            var result = source.ToMaybe<int>();
            result.MustBeNone();
            Assert.IsType<Maybe<int>>(result);
        }

        [Fact]
        public void ToSome_WithConstant_ReturnsSome()
        {
            var source = default(None);
            var result = source.ToSome(23);
            var value = result.MustBeSome();
            Assert.Equal(23, value);
        }

        [Fact]
        public void ToSome_WithProvider_ReturnsSome()
        {
            var source = default(None);
            var result = source.ToSome(() => 23);
            var value = result.MustBeSome();
            Assert.Equal(23, value);
        }
    }
}