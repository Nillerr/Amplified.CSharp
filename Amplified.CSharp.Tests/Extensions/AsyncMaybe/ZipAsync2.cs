using System;
using System.Linq;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe__ZipAsync2
    {
        #region Validation

        [Fact]
        public async Task WithNullZipper_ThrowsException()
        {
            var first = AsyncMaybe<int>.None();
            var second = AsyncMaybe<int>.None();

            // ReSharper disable once AssignNullToNotNullAttribute
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await first.ZipAsync<int, int, object>(second, null));
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

            Task<ValueTuple<int, int>> Zipper(int f, int s) => Task.FromResult((f, s));

            var result = await first.ZipAsync(second, Zipper);
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
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

            var result = await first.ZipAsync(second, (f, s) => Task.FromResult((first: f, second: s)));
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.first);
            Assert.Equal(secondValue, value.second);
        }
        
        [Fact]
        public async Task WhenAnyIsNone_ReturnsNone()
        {
            AsyncMaybe<int> CreateMaybe(int flag) => flag == 0 ? AsyncMaybe<int>.None() : AsyncMaybe<int>.Some(flag);

            const int numberOfSources = 2;
            const int numberOfStates = 2;
            const int expectedSomeInvocations = 1;
            var someInvocations = 0;

            var expectedNoneInvocations = (int) Math.Pow(numberOfStates, numberOfSources) - expectedSomeInvocations;
            var noneInvocations = 0;
            
            foreach (var a in Enumerable.Range(0, 2))
            foreach (var b in Enumerable.Range(0, 2))
            {
                var first = CreateMaybe(a);
                var second = CreateMaybe(b);

                if (await first.IsSome && await second.IsSome)
                {
                    someInvocations++;
                    continue;
                }

                var result = await first.ZipAsync(second, (s1, s2) => Task.FromResult((s1, s2)));
                result.MustBeNone();

                noneInvocations++;
            }

            Assert.Equal(expectedNoneInvocations, noneInvocations);
            Assert.Equal(expectedSomeInvocations, someInvocations);
        }

        #endregion
    }
}