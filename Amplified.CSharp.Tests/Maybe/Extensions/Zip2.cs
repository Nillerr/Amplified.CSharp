using System;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Zip2
    {
        #region Validation

        [Fact]
        public void WithNullZipper_ThrowsException()
        {
            var first = Maybe<int>.None();
            var second = Maybe<int>.None();

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => first.Zip<int, int, object>(second, null));
        }
        
        #endregion

        #region Method Reference

        [Fact]
        public void WhenFirstIsSome_AndSecondIsSome_UsingMethodReference_ReturnsSome()
        {
            const int firstValue = 23;
            var first = Maybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = Maybe<int>.Some(secondValue);

            ValueTuple<int, int> Zipper(int f, int s) => (f, s);

            var result = first.Zip(second, Zipper);
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.Item1);
            Assert.Equal(secondValue, value.Item2);
        }
        
        #endregion
        
        #region Lambda
        
        [Fact]
        public void WhenFirstIsSome_AndSecondIsSome_UsingLambda_ReturnsSome()
        {
            const int firstValue = 23;
            var first = Maybe<int>.Some(firstValue);

            const int secondValue = 6589;
            var second = Maybe<int>.Some(secondValue);

            var result = first.Zip(second, (f, s) => (first: f, second: s));
            var value = result.MustBeSome();
            Assert.Equal(firstValue, value.first);
            Assert.Equal(secondValue, value.second);
        }
        
        [Fact]
        public void WhenFirstIsSome_AndSecondIsNone_UsingLambda_ReturnsNone()
        {
            const int firstValue = 23;
            var first = Maybe<int>.Some(firstValue);

            var second = Maybe<int>.None();

            var result = first.Zip(second, (f, s) => (first: f, second: s));
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenFirstIsNone_AndSecondIsSome_UsingLambda_ReturnsNone()
        {
            var first = Maybe<int>.None();

            const int secondValue = 6589;
            var second = Maybe<int>.Some(secondValue);

            var result = first.Zip(second, (f, s) => (first: f, second: s));
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenFirstIsNone_AndSecondIsNone_UsingLambda_ReturnsNone()
        {
            var first = Maybe<int>.None();
            var second = Maybe<int>.None();

            var result = first.Zip(second, (f, s) => (first: f, second: s));
            result.MustBeNone();
        }

        #endregion
    }
}