using System.Collections.Generic;
using System.Linq;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeToEnumerableExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this Maybe<T> source)
        {
            return source.Match(
                some => Enumerable.Repeat(some, 1),
                none => none.ToEnumerable<T>()
            );
        }
    }
}