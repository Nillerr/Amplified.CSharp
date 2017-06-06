using System;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Zip3
    {
        #region Validation

        [Fact]
        public async Task WithNullZipper_ThrowsException()
        {
            var first = AsyncMaybe<int>.None();
            var second = AsyncMaybe<int>.None();
            var third = AsyncMaybe<int>.None();

            // ReSharper disable once AssignNullToNotNullAttribute
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await first.Zip<int, int, int, object>(second, third, null));
        }
        
        #endregion

        #region Method Reference

        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsSome_AndThirdIsSome_UsingMethodReference_ReturnsSome()
        {
            const int firstValue = 23;
            var first = AsyncMaybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = AsyncMaybe<int>.Some(secondValue);

            const int thirdValue = 2136589;
            var third = AsyncMaybe<int>.Some(thirdValue);

            ValueTuple<int, int, int> Zipper(int f, int s, int t) => (f, s, t);

            var result = await first.Zip(second, third, Zipper);
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
            Assert.Equal(thirdValue, value.Item3);
        }
        
        #endregion
        
        #region Lambda
        
        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsSome_AndThirdIsSome_ReturnsSome()
        {
            const int firstValue = 23;
            var first = AsyncMaybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = AsyncMaybe<int>.Some(secondValue);

            const int thirdValue = 2136589;
            var third = AsyncMaybe<int>.Some(thirdValue);

            var result = await first.Zip(second, third, (f, s, t) => (first: f, second: s, third: t));
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.first);
            Assert.Equal(secondValue, value.second);
            Assert.Equal(thirdValue, value.third);
        }
        
        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsSome_AndThirdIsNone_ReturnsNone()
        {
            var first = AsyncMaybe<int>.Some(451);
            var second = AsyncMaybe<int>.Some(548);
            var third = AsyncMaybe<int>.None();

            var result = await first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsNone_AndThirdIsSome_ReturnsNone()
        {
            var first = AsyncMaybe<int>.Some(451);
            var second = AsyncMaybe<int>.None();
            var third = AsyncMaybe<int>.Some(123);

            var result = await first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsNone_AndThirdIsNone_ReturnsNone()
        {
            var first = AsyncMaybe<int>.Some(451);
            var second = AsyncMaybe<int>.None();
            var third = AsyncMaybe<int>.None();

            var result = await first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public async Task WhenFirstIsNone_AndSecondIsSome_AndThirdIsSome_ReturnsNone()
        {
            var first = AsyncMaybe<int>.None();
            var second = AsyncMaybe<int>.Some(548);
            var third = AsyncMaybe<int>.Some(123);

            var result = await first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public async Task WhenFirstIsNone_AndSecondIsSome_AndThirdIsNone_ReturnsNone()
        {
            var first = AsyncMaybe<int>.None();
            var second = AsyncMaybe<int>.Some(123);
            var third = AsyncMaybe<int>.None();

            var result = await first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public async Task WhenFirstIsNone_AndSecondIsNone_AndThirdIsSome_ReturnsNone()
        {
            var first = AsyncMaybe<int>.None();
            var second = AsyncMaybe<int>.None();
            var third = AsyncMaybe<int>.Some(123);

            var result = await first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }

        #endregion
    }
}