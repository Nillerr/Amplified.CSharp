using System;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Zip2
    {
        #region Validation

        [Fact]
        public async Task WithNullZipper_ThrowsException()
        {
            var first = AsyncMaybe<int>.None();
            var second = AsyncMaybe<int>.None();

            // ReSharper disable once AssignNullToNotNullAttribute
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await first.Zip<int, int, object>(second, null));
        }
        
        #endregion

        #region Method Reference

        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsSome_UsingMethodReference_ReturnsSome()
        {
            const int firstValue = 23;
            var first = AsyncMaybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = AsyncMaybe<int>.Some(secondValue);

            ValueTuple<int, int> Zipper(int f, int s) => (f, s);

            var result = await first.Zip(second, Zipper);
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
        }
        
        #endregion
        
        #region Lambda
        
        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsSome_UsingLambda_ReturnsSome()
        {
            const int firstValue = 23;
            var first = AsyncMaybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = AsyncMaybe<int>.Some(secondValue);

            var result = await first.Zip(second, (f, s) => (first: f, second: s));
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.first);
            Assert.Equal(secondValue, value.second);
        }
        
        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsNone_UsingLambda_ReturnsNone()
        {
            const int firstValue = 23;
            var first = AsyncMaybe<int>.Some(firstValue);

            var second = AsyncMaybe<int>.None();

            var result = await first.Zip(second, (f, s) => (first: f, second: s));
            result.MustBeNone();
        }
        
        [Fact]
        public async Task WhenFirstIsNone_AndSecondIsSome_UsingLambda_ReturnsNone()
        {
            var first = AsyncMaybe<int>.None();

            const int secondValue = 6589;
            var second = AsyncMaybe<int>.Some(secondValue);

            var result = await first.Zip(second, (f, s) => (first: f, second: s));
            result.MustBeNone();
        }
        
        [Fact]
        public async Task WhenFirstIsNone_AndSecondIsNone_UsingLambda_ReturnsNone()
        {
            var first = AsyncMaybe<int>.None();
            var second = AsyncMaybe<int>.None();

            var result = await first.Zip(second, (f, s) => (first: f, second: s));
            result.MustBeNone();
        }

        #endregion
    }
}