using System.Collections.Generic;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeToEnumerableExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this Maybe<T> source)
        {
            return source.AsSome().Match(
                some => some.ToEnumerable(),
                none => none.ToEnumerable<T>()
            );
        }
    }
}