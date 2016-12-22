using System.Collections.Generic;
using System.Linq;

namespace Amplified.CSharp.Extensions
{
    public static class SomeToEnumerableExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this Some<T> source)
        {
            return Enumerable.Repeat(source.Value, 1);
        }
    }
}