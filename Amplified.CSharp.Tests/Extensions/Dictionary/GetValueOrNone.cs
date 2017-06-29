using System.Collections.Generic;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Dictionary__GetValueOrNone
    {
        [Fact]
        public void WithPresentKey_ReturnsSome()
        {
            const int expected = 2342;
            const string key = "MyKey";
            var source = new Dictionary<string, int> {[key] = expected};
            var result = source.GetValueOrNone(key);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }
        
        [Fact]
        public void WithMissingKey_ReturnsNone()
        {
            const string key = "MyKey";
            var source = new Dictionary<string, int> {["SomeOtherKey"] = 7897425};
            var result = source.GetValueOrNone(key);
            result.MustBeNone();
        }
        
        [Fact]
        public void ReadOnly_WithPresentKey_ReturnsSome()
        {
            const int expected = 2342;
            const string key = "MyKey";
            IReadOnlyDictionary<string, int> source = new Dictionary<string, int> {[key] = expected};
            var result = source.GetValueOrNone(key);
            var value = result.MustBeSome();
            Assert.Equal(expected, value);
        }
        
        [Fact]
        public void ReadOnly_WithMissingKey_ReturnsNone()
        {
            const string key = "MyKey";
            IReadOnlyDictionary<string, int> source = new Dictionary<string, int> {["SomeOtherKey"] = 7897425};
            var result = source.GetValueOrNone(key);
            result.MustBeNone();
        }
    }
}