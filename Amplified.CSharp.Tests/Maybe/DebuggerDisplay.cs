using System.Reflection;
using Xunit;

namespace Amplified.CSharp
{
    public class DebuggerDisplay
    {
        [Fact]
        public void WhenSome_ReturnsSomeToString()
        {
            const int value = 5;
            var some = Maybe.Some(value);
            var actual = GetDebuggerDisplay(some);
            Assert.Equal($"Some({value})", actual);
        }

        [Fact]
        public void WhenNone_ReturnsNone()
        {
            Maybe<int> none = Maybe.None();
            var actual = GetDebuggerDisplay(none);
            Assert.Equal("None", actual);
        }

        private static string GetDebuggerDisplay<T>(Maybe<T> maybe)
        {
            var property = maybe.GetType()
                .GetTypeInfo()
                .GetProperty("DebuggerDisplay", BindingFlags.Instance | BindingFlags.NonPublic);

            var getter = property.GetMethod;
            var actual = getter.Invoke(maybe, new object[0]);
            
            return (string) actual;
        }
    }
}