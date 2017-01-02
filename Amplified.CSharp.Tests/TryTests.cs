using System;
using Xunit;

namespace Amplified.CSharp
{
    public class TryTests
    {
        [Fact]
        public void StaticFactory_WithResult_IsResult()
        {
            const int result = 1;
            var attempt = Try.Result(result);
            attempt.AssertIsResult(result);
        }

        [Fact]
        public void Constructor_WithResult_IsResult()
        {
            const int result = 1;
            var attempt = new Try<int>(result);
            attempt.AssertIsResult(result);
        }

        [Fact]
        public void StaticFactory_WithException_IsException()
        {
            var attempt = Try.Exception<int>(new Exception());
            attempt.AssertIsException();
        }

        [Fact]
        public void Constructor_WithException_IsException()
        {
            var attempt = new Try<int>(new Exception());
            attempt.AssertIsException();
        }

        [Fact]
        public void StaticFactory_WithReturningLambda_IsResult()
        {
            const int result = 2;
            var attempt = Try._(() => result);
            attempt.AssertIsResult(result);
        }

        [Fact]
        public void StaticFactory_WithThrowingLambda_IsException()
        {
            var attempt = Try._<int>(() => { throw new Exception(); });
            attempt.AssertIsException();
        }
    }
}