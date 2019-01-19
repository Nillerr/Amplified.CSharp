using System;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__ZipFunc
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
            const int firstValue = 2;
            const int secondValue = 3;
            var expectedValue = (firstValue, firstValue + secondValue);
            
            var first = Maybe<int>.Some(firstValue);

            ValueTuple<int, int> Zipper(int f, int s) => (f, s);

            var result = first.Zip(f => Maybe.Some(f + secondValue), Zipper);
            var value = result.MustBeSome();
            Assert.Equal(expectedValue.Item1, value.Item1);
            Assert.Equal(expectedValue.Item2, value.Item2);
        }
        
        #endregion
        
        #region Lambda
        
        [Fact]
        public void WhenFirstIsSome_AndSecondIsSome_UsingLambda_ReturnsSome()
        {
            const int firstValue = 2;
            const int secondValue = 3;
            var expectedValue = (firstValue, firstValue + secondValue);
            
            var first = Maybe<int>.Some(firstValue);

            var result = first.Zip(f => Maybe.Some(f + secondValue), (f, s) => (f, s));
            var value = result.MustBeSome();
            Assert.Equal(expectedValue.Item1, value.Item1);
            Assert.Equal(expectedValue.Item2, value.Item2);
        }
        
        [Fact]
        public void WhenFirstIsSome_AndSecondIsNone_UsingLambda_ReturnsNone()
        {
            const int firstValue = 23;
            var first = Maybe<int>.Some(firstValue);

            var result = first.Zip(_ => Maybe<int>.None(), (f, s) => (f, s));
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenFirstIsNone_AndSecondIsSome_UsingLambda_ReturnsNone()
        {
            var first = Maybe<int>.None();

            var result = first.Zip(_ => Maybe<int>.Some(5), (f, s) => (f, s));
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenFirstIsNone_AndSecondIsNone_UsingLambda_ReturnsNone()
        {
            var first = Maybe<int>.None();

            var result = first.Zip(f => Maybe<int>.None(), (f, s) => (f, s));
            result.MustBeNone();
        }

        #endregion
    }
}