using System;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__OrThrow
    {
        [Fact]
        public void OnSome_ReturnsValue()
        {
            const int expected = 6;
            var source = Maybe<int>.Some(expected);
            var result = source.OrThrow(new NotImplementedException());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnSome_WithLambda_ReturnsValue()
        {
            const int expected = 6;
            var source = Maybe<int>.Some(expected);
            var result = source.OrThrow(() => new NotImplementedException());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnNone_ThrowsException()
        {
            var source = Maybe<int>.None();
            Assert.Throws<NotImplementedException>(() => source.OrThrow(new NotImplementedException()));
        }

        [Fact]
        public void OnNone_WithLambda_ThrowsException()
        {
            var source = Maybe<int>.None();
            Assert.Throws<NotImplementedException>(() => source.OrThrow(() => new NotImplementedException()));
        }
    }
}
