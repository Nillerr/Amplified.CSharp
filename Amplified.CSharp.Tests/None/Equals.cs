using System;
using Amplified.CSharp.Internal;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class None__Equals
    {
        #region bool Equals(None other)
        
        [Fact]
        public void WithNone_ReturnsTrue()
        {
            var source = new None();
            var other = new None();
            var result = source.Equals(other);
            Assert.True(result);
        }
        
        #endregion

        #region bool Equals<T>(Maybe<T> other)
        
        [Fact]
        public void WithMaybeNone_ReturnsTrue()
        {
            var source = new None();
            var other = Maybe<int>.None();
            var result = source.Equals(other);
            Assert.True(result);
        }

        [Fact]
        public void WithMaybeSome_ReturnsFalse()
        {
            var source = new None();
            var other = Maybe<int>.Some(1);
            var result = source.Equals(other);
            Assert.False(result);
        }
        
        #endregion

        #region bool Equals(IMaybe other)
        
        [Fact]
        public void WithIMaybeNone_ReturnsTrue()
        {
            var source = new None();
            var other = Maybe<int>.None();
            var result = source.Equals((IMaybe) other);
            Assert.True(result);
        }

        [Fact]
        public void WithIMaybeSome_ReturnsFalse()
        {
            var source = new None();
            var other = Maybe<int>.Some(1);
            var result = source.Equals((IMaybe) other);
            Assert.False(result);
        }

        [Fact]
        public void WithIMaybeNull_ThrowsArgumentNullException()
        {
            var source = new None();
            IMaybe other = null;
            
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => source.Equals(other));
        }

        #endregion
        
        #region bool Equals(object other) 
        
        [Fact]
        public void WithObjectNone_ReturnsTrue()
        {
            var source = new None();
            var other = new None();
            var result = source.Equals((object) other);
            Assert.True(result);
        }
        
        [Fact]
        public void WithObjectNull_ReturnsFalse()
        {
            var source = new None();
            object other = null;
            
            // ReSharper disable once ExpressionIsAlwaysNull
            var result = source.Equals(other);
            Assert.False(result);
        }

        [Fact]
        public void WithObjectMaybeNone_ReturnsTrue()
        {
            var source = new None();
            var other = (object) Maybe<int>.None();
            var result = source.Equals(other);
            Assert.True(result);
        }

        [Fact]
        public void WithObjectMaybeSome_ReturnsFalse()
        {
            var source = new None();
            var other = (object) Maybe<int>.Some(1);
            var result = source.Equals(other);
            Assert.False(result);
        }
        
        #endregion
    }
}
