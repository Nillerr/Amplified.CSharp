using System.Globalization;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Parse
    {
        #region Maybe<byte> Parse(string)
        
        [Fact]
        public void WithNull_ReturnsNone()
        {
            var result = Maybe.Parse<byte>(byte.TryParse, null);
            result.MustBeNone();
        }

        [Fact]
        public void WithEmptyString_ReturnsNone()
        {
            var result = Maybe.Parse<byte>(byte.TryParse, string.Empty);
            result.MustBeNone();
        }

        [Fact]
        public void WithWhitespace_ReturnsNone()
        {
            var result = Maybe.Parse<byte>(byte.TryParse, " \t ");
            result.MustBeNone();
        }

        [Fact]
        public void WithNumberOutsideRange_ReturnNone()
        {
            const int expected = byte.MaxValue + 1;
            var result = Maybe.Parse<byte>(byte.TryParse, $"{expected}");
            result.MustBeNone();
        }

        [Fact]
        public void WithNumberInRange_ReturnSome()
        {
            const byte expected = byte.MaxValue;
            var result = Maybe.Parse<byte>(byte.TryParse, $"{expected}");
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }
        
        #endregion
        
        #region Maybe<byte> Parse(string, NumberStyles, IFormatProvider)
        
        [Fact]
        public void WithNull_AndNumberFormat_AndCurrentFormatProvider_ReturnsNone()
        {
            var result = Maybe.Parse<byte>(byte.TryParse, null, NumberStyles.AllowThousands, CultureInfo.CurrentCulture);
            result.MustBeNone();
        }

        [Fact]
        public void WithEmptyString_AndNumberFormat_AndCurrentFormatProvider_ReturnsNone()
        {
            var result = Maybe.Parse<byte>(byte.TryParse, string.Empty, NumberStyles.AllowThousands, CultureInfo.CurrentCulture);
            result.MustBeNone();
        }

        [Fact]
        public void WithWhitespace_AndNumberFormat_AndCurrentFormatProvider_ReturnsNone()
        {
            var result = Maybe.Parse<byte>(byte.TryParse, " \t ", NumberStyles.AllowThousands, CultureInfo.CurrentCulture);
            result.MustBeNone();
        }

        [Fact]
        public void WithNumberOutsideRange_AndNumberFormat_AndCurrentFormatProvider_ReturnNone()
        {
            const int expected = byte.MaxValue + 1;
            var result = Maybe.Parse<byte>(byte.TryParse, $"{expected}", NumberStyles.AllowThousands, CultureInfo.CurrentCulture);
            result.MustBeNone();
        }

        [Fact]
        public void WithNumberInRange_AndNumberFormat_AndCurrentFormatProvider_ReturnSome()
        {
            const byte expected = byte.MaxValue;
            var result = Maybe.Parse<byte>(byte.TryParse, $"{expected}", NumberStyles.AllowThousands, CultureInfo.CurrentCulture);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }
        
        #endregion
    }
}