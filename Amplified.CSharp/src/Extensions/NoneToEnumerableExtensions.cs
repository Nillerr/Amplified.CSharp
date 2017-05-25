using System.Collections.Generic;
using System.Linq;
using Amplified.CSharp.Internal;

namespace Amplified.CSharp.Extensions
{
    public static class NoneToEnumerableExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this None source)
        {
            return Enumerable.Empty<T>();
        }
    }
}