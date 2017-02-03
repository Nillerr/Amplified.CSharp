using System;
using Xunit;

namespace Amplified.CSharp.Util
{
    public class FailTests
    {
        [Fact]
        public void FailWith_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => Fail.With(new object()));
            Assert.Throws<InvalidOperationException>(() => Fail.With(1));
        }
    }
}