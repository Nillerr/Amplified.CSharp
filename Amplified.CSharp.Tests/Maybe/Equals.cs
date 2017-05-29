using System;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Equals
    {
        #region bool Equals(Maybe<T> other)
        
        [Fact]
        public void Some_EqualsOtherSomeWithSameValue()
        {
            var source = Maybe<int>.Some(2);
            var other = Maybe<int>.Some(2);
            
            var equals = source.Equals(other);
            Assert.True(equals);
            Assert.Equal(source, other);
        }

        [Fact]
        public void Some_NotEqualsOtherSomeWithDifferentValue()
        {
            var source = Maybe<int>.Some(1);
            var other = Maybe<int>.Some(2);

            var equals = source.Equals(other);
            Assert.False(equals);
            Assert.NotEqual(source, other);
        }

        [Fact]
        public void None_EqualsOtherNone()
        {
            var source = Maybe<int>.None();
            var other = Maybe<int>.None();

            var equals = source.Equals(other);
            Assert.True(equals);
            Assert.Equal(source, other);
        }

        [Fact]
        public void None_NotEqualToOtherSome()
        {
            var source = Maybe<int>.None();
            var other = Maybe<int>.Some(1);

            var equals = source.Equals(other);
            Assert.False(equals);
            Assert.NotEqual(source, other);
        }

        [Fact]
        public void None_IsEqualTo_MaybeNone()
        {
            var source = Maybe<int>.None();
            var other = new None();

            var equals = source.Equals(other);
            Assert.True(equals);
            Assert.Equal(source, other);
        }

        [Fact]
        public void None_IsNotEqualTo_MaybeSome()
        {
            var source = Maybe<int>.Some(1);
            var other = new None();

            var equals = source.Equals(other);
            Assert.False(equals);
            Assert.NotEqual(source, other);
        }
        
        #endregion

        #region bool Equals(object other)
        
        [Fact]
        public void WithMaybeSomeObject_WithSameValue()
        {
            var value = new object();
            
            var source = Maybe<object>.Some(value);
            var other = (object) Maybe<object>.Some(value);

            var result = source.Equals(other);
            Assert.True(result);
        }

        [Fact]
        public void WithMaybeSomeObject_WithDifferentValue()
        {
            var source = Maybe<object>.Some(new object());
            var other = (object) Maybe<object>.Some(new object());

            var result = source.Equals(other);
            Assert.False(result);
        }

        [Fact]
        public void WithNoneObject()
        {
            var source = Maybe<object>.Some(new object());
            var other = (object) Maybe<object>.None();

            var result = source.Equals(other);
            Assert.False(result);
        }

        [Fact]
        public void WithNullOtherObject()
        {
            var source = Maybe<object>.Some(new object());
            var other = (object) null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var result = source.Equals(other);
            Assert.False(result);
        }
        
        #endregion
    }
}
