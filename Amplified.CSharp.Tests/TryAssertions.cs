using Xunit;

namespace Amplified.CSharp
{
    public static class TryAssertions
    {
        public static void AssertIsResult<T>(this Try<T> @try, T result)
        {
            Assert.True(@try.IsResult);
            Assert.False(@try.IsException);
            Assert.Equal(@try, result);
        }

        public static void AssertIsException<T>(this Try<T> @try)
        {
            Assert.False(@try.IsResult);
            Assert.True(@try.IsException);
        }
    }
}