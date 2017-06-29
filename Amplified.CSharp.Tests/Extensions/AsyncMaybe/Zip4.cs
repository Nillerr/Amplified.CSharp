using System;
using System.Linq;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__Zip4
    {
        #region Validation

        [Fact]
        public async Task WithNullZipper_ThrowsException()
        {
            var first = AsyncMaybe<int>.None();
            var second = AsyncMaybe<int>.None();
            var third = AsyncMaybe<int>.None();
            var fourth = AsyncMaybe<int>.None();

            // ReSharper disable once AssignNullToNotNullAttribute
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await first.Zip<int, int, int, int, object>(second, third, fourth, null));
        }
        
        #endregion

        #region Method Reference

        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsSome_AndThirdIsSome_AndFourthIsSome_UsingMethodReference_ReturnsSome()
        {
            const int firstValue = 23;
            var first = AsyncMaybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = AsyncMaybe<int>.Some(secondValue);

            const int thirdValue = 2136589;
            var third = AsyncMaybe<int>.Some(thirdValue);

            const int fourthValue = 547894;
            var fourth = AsyncMaybe<int>.Some(fourthValue);

            ValueTuple<int, int, int, int> Zipper(int f, int s, int t, int f4) => (f, s, t, f4);

            var result = await first.Zip(second, third, fourth, Zipper);
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
            Assert.Equal(thirdValue, value.Item3);
            Assert.Equal(fourthValue, value.Item4);
        }
        
        #endregion
        
        #region Lambda
        
        [Fact]
        public async Task WhenFirstIsSome_AndSecondIsSome_AndThirdIsSome_AndFourthIsSome_ReturnsSome()
        {
            const int firstValue = 23;
            var first = AsyncMaybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = AsyncMaybe<int>.Some(secondValue);

            const int thirdValue = 2136589;
            var third = AsyncMaybe<int>.Some(thirdValue);

            const int fourthValue = 547894;
            var fourth = AsyncMaybe<int>.Some(fourthValue);

            var result = await first.Zip(second, third, fourth, (f, s, t, f4) => (first: f, second: s, third: t, fourth: f4));
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.first);
            Assert.Equal(secondValue, value.second);
            Assert.Equal(thirdValue, value.third);
            Assert.Equal(fourthValue, value.fourth);
        }
        
        [Fact]
        public async Task WhenAnyIsNone_ReturnsNone()
        {
            AsyncMaybe<int> CreateMaybe(int flag) => flag == 0 ? AsyncMaybe<int>.None() : AsyncMaybe<int>.Some(flag);

            const int expectedSomeInvocations = 1;
            var someInvocations = 0;

            var expectedNoneInvocations = (int) Math.Pow(2, 4) - expectedSomeInvocations;
            var noneInvocations = 0;
            
            foreach (var a in Enumerable.Range(0, 2))
            foreach (var b in Enumerable.Range(0, 2))
            foreach (var c in Enumerable.Range(0, 2))
            foreach (var d in Enumerable.Range(0, 2))
            {
                var first = CreateMaybe(a);
                var second = CreateMaybe(b);
                var third = CreateMaybe(c);
                var fourth = CreateMaybe(d);

                if (await first.IsSome && await second.IsSome && await third.IsSome && await fourth.IsSome)
                {
                    someInvocations++;
                    continue;
                }

                var result = await first.Zip(second, third, fourth, (s1, s2, s3, s4) => (s1, s2, s3, s4));
                result.MustBeNone();

                noneInvocations++;
            }

            Assert.Equal(expectedNoneInvocations, noneInvocations);
            Assert.Equal(expectedSomeInvocations, someInvocations);
        }

        #endregion
    }
}