using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeToEnumerableExtensions
    {
        public static Task<IEnumerable<T>> ToEnumerable<T>(this AsyncMaybe<T> source)
        {
            return source.Match(
                some => Enumerable.Repeat(some, 1),
                none => none.ToEnumerable<T>()
            );
        }
    }
}
