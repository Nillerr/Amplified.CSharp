using System;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
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
            var result = source.OrThrow(new ExpectedException());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnSome_WithLambda_ReturnsValue()
        {
            const int expected = 6;
            var source = Maybe<int>.Some(expected);
            var result = source.OrThrow(() => new ExpectedException());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnNone_ThrowsException()
        {
            var source = Maybe<int>.None();
            Assert.Throws<ExpectedException>(() => source.OrThrow(new ExpectedException()));
        }

        [Fact]
        public void OnNone_WithLambda_ThrowsException()
        {
            var source = Maybe<int>.None();
            Assert.Throws<ExpectedException>(() => source.OrThrow(() => new ExpectedException()));
        }
    }
}
