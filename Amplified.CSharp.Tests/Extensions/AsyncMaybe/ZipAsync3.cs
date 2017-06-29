using System;
using System.Linq;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__ZipAsync3
    {
        #region Validation

        [Fact]
        public async Task WithNullZipper_ThrowsException()
        {
            var first = AsyncMaybe<int>.None();
            var second = AsyncMaybe<int>.None();
            var third = AsyncMaybe<int>.None();

            // ReSharper disable once AssignNullToNotNullAttribute
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await first.ZipAsync<int, int, int, object>(second, third, null));
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

            Task<ValueTuple<int, int, int>> Zipper(int v1, int v2, int v3) => Task.FromResult((v1, v2, v3));

            var result = await first.ZipAsync(second, third, Zipper);
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
            Assert.Equal(thirdValue, value.Item3);
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

            var result = await first.ZipAsync(second, third, (v1, v2, v3) => Task.FromResult((v1, v2, v3)));
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
            Assert.Equal(thirdValue, value.Item3);
        }
        
        [Fact]
        public async Task WhenAnyIsNone_ReturnsNone()
        {
            AsyncMaybe<int> CreateMaybe(int flag) => flag == 0 ? AsyncMaybe<int>.None() : AsyncMaybe<int>.Some(flag);

            const int numberOfSources = 3;
            const int numberOfStates = 2;
            const int expectedSomeInvocations = 1;
            var someInvocations = 0;

            var expectedNoneInvocations = (int) Math.Pow(numberOfStates, numberOfSources) - expectedSomeInvocations;
            var noneInvocations = 0;
            
            foreach (var a in Enumerable.Range(0, 2))
            foreach (var b in Enumerable.Range(0, 2))
            foreach (var c in Enumerable.Range(0, 2))
            {
                var first = CreateMaybe(a);
                var second = CreateMaybe(b);
                var third = CreateMaybe(c);

                if (await first.IsSome && await second.IsSome && await third.IsSome)
                {
                    someInvocations++;
                    continue;
                }

                var result = await first.ZipAsync(second, third, (v1, v2, v3) => Task.FromResult((v1, v2, v3)));
                result.MustBeNone();

                noneInvocations++;
            }

            Assert.Equal(expectedNoneInvocations, noneInvocations);
            Assert.Equal(expectedSomeInvocations, someInvocations);
        }

        #endregion
    }
}