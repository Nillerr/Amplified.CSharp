using System;
using System.Linq;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__ZipAsync4
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
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await first.ZipAsync<int, int, int, int, object>(second, third, fourth, null));
        }
        
        #endregion

        #region Method Reference

        [Fact]
        public async Task WhenAllAreSome_UsingMethodReference_ReturnsSome()
        {
            const int firstValue = 23;
            var first = AsyncMaybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = AsyncMaybe<int>.Some(secondValue);

            const int thirdValue = 236589;
            var third = AsyncMaybe<int>.Some(thirdValue);

            const int fourthValue = 1236589;
            var fourth = AsyncMaybe<int>.Some(fourthValue);

            Task<ValueTuple<int, int, int, int>> Zipper(int v1, int v2, int v3, int v4) => Task.FromResult((v1, v2, v3, v4));

            var result = await first.ZipAsync(second, third, fourth, Zipper);
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
            Assert.Equal(thirdValue, value.Item3);
            Assert.Equal(fourthValue, value.Item4);
        }
        
        #endregion
        
        #region Lambda
        
        [Fact]
        public async Task WhenAllAreSome_ReturnsSome()
        {
            const int firstValue = 23;
            var first = AsyncMaybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = AsyncMaybe<int>.Some(secondValue);

            const int thirdValue = 236589;
            var third = AsyncMaybe<int>.Some(thirdValue);

            const int fourthValue = 1236589;
            var fourth = AsyncMaybe<int>.Some(fourthValue);

            var result = await first.ZipAsync(second, third, fourth, (v1, v2, v3, v4) => Task.FromResult((v1, v2, v3, v4)));
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
            Assert.Equal(thirdValue, value.Item3);
            Assert.Equal(fourthValue, value.Item4);
        }
        
        [Fact]
        public async Task WhenAnyIsNone_ReturnsNone()
        {
            AsyncMaybe<int> CreateMaybe(int flag) => flag == 0 ? AsyncMaybe<int>.None() : AsyncMaybe<int>.Some(flag);

            const int numberOfSources = 4;
            const int numberOfStates = 2;
            const int expectedSomeInvocations = 1;
            var someInvocations = 0;

            var expectedNoneInvocations = (int) Math.Pow(numberOfStates, numberOfSources) - expectedSomeInvocations;
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

                var result = await first.ZipAsync(second, third, fourth, (v1, v2, v3, v4) => Task.FromResult((v1, v2, v3, v4)));
                result.MustBeNone();

                noneInvocations++;
            }

            Assert.Equal(expectedNoneInvocations, noneInvocations);
            Assert.Equal(expectedSomeInvocations, someInvocations);
        }

        #endregion
    }
}