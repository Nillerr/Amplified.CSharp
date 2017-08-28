using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Amplified.CSharp.ComponentModel;
using Xunit;

namespace Amplified.CSharp.TypeConversion
{
    public class GetConverter
    {
        [Fact]
        public void ReturnsMaybeConverter()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            Assert.IsType<MaybeConverter>(converter);
        }
        
        [Fact]
        public void TwiceForSameType_ReturnsSameConverter()
        {
            var converter1 = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var converter2 = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            Assert.Same(converter1, converter2);
        }
        
        [Fact]
        public void ForDifferentTypes_ReturnsDifferentConverters()
        {
            var converter1 = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var converter2 = TypeDescriptor.GetConverter(typeof(Maybe<object>));
            Assert.NotSame(converter1, converter2);
        }
        
        [Fact]
        public void CanConvertFromInt()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var canConvert = converter.CanConvertFrom(typeof(int));
            Assert.True(canConvert);
        }
        
        [Fact]
        public void ConvertFromInt()
        {
            const int expected = 5;
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var value = converter.ConvertFrom(expected);
            Assert.Equal(Maybe.Some(expected), value);
        }

        [Fact]
        public void CanConvertFromNullableInt()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var canConvert = converter.CanConvertFrom(typeof(int?));
            Assert.True(canConvert);
        }
        
        [Fact]
        public void ConvertFromNonNullNullableInt()
        {
            var expected = (int?) 5;
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var result = (Maybe<int>) converter.ConvertFrom(expected);
            Assert.Equal(expected, result.OrFail());
        }
        
        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void ConvertFromNullNullableInt()
        {
            int? expected = null;
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var value = converter.ConvertFrom(expected);
            Assert.Equal(Maybe.OfNullable(expected), value);
        }

        [Fact]
        public void CanConvertToInt()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var canConvert = converter.CanConvertTo(typeof(int));
            Assert.True(canConvert);
        }

        [Fact]
        public void CanConvertToNullableInt()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var canConvert = converter.CanConvertTo(typeof(int?));
            Assert.True(canConvert);
        }

        [Fact]
        public void CanConvertFromObject()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<object>));
            var canConvert = converter.CanConvertFrom(typeof(object));
            Assert.True(canConvert);
        }

        [Fact]
        public void ConvertFromNonNullObject_IsSameInstance()
        {
            var expected = new object();
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<object>));
            var result = converter.ConvertFrom(expected);
            Assert.Equal(Maybe.Some(expected), result);
        }

        [Fact]
        public void ConvertFromNullObject_ReturnsNone()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<object>));
            var result = converter.ConvertFrom(null);
            Assert.Equal(Maybe.None(), result);
        }

        [Fact]
        public void CanConvertToObject()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<object>));
            var canConvert = converter.CanConvertTo(typeof(object));
            Assert.True(canConvert);
        }

        [Fact]
        public void ConvertToNonNullObject()
        {
            var expected = new object();
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<object>));
            var source = Maybe.Some(expected);
            var result = converter.ConvertTo(source, typeof(object));
            Assert.Same(expected, result);
        }

        [Fact]
        public void ConvertToNullObject()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<object>));
            var source = Maybe.None<object>();
            var result = converter.ConvertTo(source, typeof(object));
            Assert.Null(result);
        }

        [Fact]
        public void CanConvertFromString()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var canConvert = converter.CanConvertFrom(typeof(string));
            Assert.True(canConvert);
        }

        [Fact]
        public void ConvertFromString()
        {
            const string str = "25";
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var result = converter.ConvertFrom(str);
            Assert.Equal(Maybe.Some(25), result);
        }

        [Fact]
        public void ConvertFromEmptyString()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var result = (Maybe<int>) converter.ConvertFrom(string.Empty);
            Assert.Equal(Maybe.None<int>(), result);
        }

        [Fact]
        public void ConvertFromNullString()
        {
            string str = null;
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var result = (Maybe<int>) converter.ConvertFrom(str);
            Assert.Equal(Maybe.None<int>(), result);
        }

        [Fact]
        public void CanConvertToString()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var canConvert = converter.CanConvertTo(typeof(string));
            Assert.True(canConvert);
        }

        [Fact]
        public void ConvertToString()
        {
            const int expected = 25553;
            var converter = TypeDescriptor.GetConverter(typeof(Maybe<int>));
            var source = Maybe.Some(expected);
            var result = converter.ConvertTo(source, typeof(string));
            Assert.Equal($"{expected}", result);
        }
    }
}