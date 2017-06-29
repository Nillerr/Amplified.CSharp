using System;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Zip3
    {
        #region Validation

        [Fact]
        public void WithNullZipper_ThrowsException()
        {
            var first = Maybe<int>.None();
            var second = Maybe<int>.None();
            var third = Maybe<int>.None();

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => first.Zip<int, int, int, object>(second, third, null));
        }
        
        #endregion

        #region Method Reference

        [Fact]
        public void WhenFirstIsSome_AndSecondIsSome_AndThirdIsSome_UsingMethodReference_ReturnsSome()
        {
            const int firstValue = 23;
            var first = Maybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = Maybe<int>.Some(secondValue);

            const int thirdValue = 2136589;
            var third = Maybe<int>.Some(thirdValue);

            ValueTuple<int, int, int> Zipper(int f, int s, int t) => (f, s, t);

            var result = first.Zip(second, third, Zipper);
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
            Assert.Equal(thirdValue, value.Item3);
        }
        
        #endregion
        
        #region Lambda
        
        [Fact]
        public void WhenFirstIsSome_AndSecondIsSome_AndThirdIsSome_ReturnsSome()
        {
            const int firstValue = 23;
            var first = Maybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = Maybe<int>.Some(secondValue);

            const int thirdValue = 2136589;
            var third = Maybe<int>.Some(thirdValue);

            var result = first.Zip(second, third, (f, s, t) => (first: f, second: s, third: t));
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.first);
            Assert.Equal(secondValue, value.second);
            Assert.Equal(thirdValue, value.third);
        }
        
        [Fact]
        public void WhenFirstIsSome_AndSecondIsSome_AndThirdIsNone_ReturnsNone()
        {
            var first = Maybe<int>.Some(451);
            var second = Maybe<int>.Some(548);
            var third = Maybe<int>.None();

            var result = first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenFirstIsSome_AndSecondIsNone_AndThirdIsSome_ReturnsNone()
        {
            var first = Maybe<int>.Some(451);
            var second = Maybe<int>.None();
            var third = Maybe<int>.Some(123);

            var result = first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenFirstIsSome_AndSecondIsNone_AndThirdIsNone_ReturnsNone()
        {
            var first = Maybe<int>.Some(451);
            var second = Maybe<int>.None();
            var third = Maybe<int>.None();

            var result = first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenFirstIsNone_AndSecondIsSome_AndThirdIsSome_ReturnsNone()
        {
            var first = Maybe<int>.None();
            var second = Maybe<int>.Some(548);
            var third = Maybe<int>.Some(123);

            var result = first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenFirstIsNone_AndSecondIsSome_AndThirdIsNone_ReturnsNone()
        {
            var first = Maybe<int>.None();
            var second = Maybe<int>.Some(123);
            var third = Maybe<int>.None();

            var result = first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenFirstIsNone_AndSecondIsNone_AndThirdIsSome_ReturnsNone()
        {
            var first = Maybe<int>.None();
            var second = Maybe<int>.None();
            var third = Maybe<int>.Some(123);

            var result = first.Zip(second, third, (f, s, t) => (f, s, t));
            result.MustBeNone();
        }

        #endregion
    }
}