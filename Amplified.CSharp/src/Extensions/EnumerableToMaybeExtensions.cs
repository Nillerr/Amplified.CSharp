using System.Collections.Generic;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class EnumerableToMaybeExtensions
    {
        public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> source)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var list = source as IList<T>;
                if (list != null)
                {
                    if (list.Count == 1)
                    {
                        return Some(list[0]);
                    }
                }
                else
                {
                    if (enumerator.MoveNext())
                    {
                        var result = enumerator.Current;
                        if (enumerator.MoveNext() == false)
                        {
                            return Some(result);
                        }
                    }
                }
                return Maybe<T>.None;
            }
        }

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> source)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var list = source as IList<T>;
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        return Some(list[0]);
                    }
                }
                else if (enumerator.MoveNext())
                {
                    return Some(enumerator.Current);
                }
                return Maybe<T>.None;
            }
        }

        public static Maybe<T> LastOrNone<T>(this IEnumerable<T> source)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var list = source as IList<T>;
                if (list != null)
                {
                    var count = list.Count;
                    if (count > 0)
                    {
                        return Some(list[count - 1]);
                    }
                }

                var last = Maybe<T>.None;
                while (enumerator.MoveNext())
                {
                    last = Some(enumerator.Current);
                }
                return last;
            }
        }

        public static Maybe<T> ElementAtOrNone<T>(this IEnumerable<T> source, int index)
        {
            var list = source as IList<T>;
            if (list != null)
            {
                if (index < list.Count)
                {
                    return Some(list[index]);
                }
            }
            else
            {
                using (var enumerator = source.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext() == false) return Maybe<T>.None;
                        if (index == 0) return Some(enumerator.Current);
                        index--;
                    }
                }
            }
            return Maybe<T>.None;
        }

        public static Maybe<TValue> Value<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
        {
            return source.ContainsKey(key) ? Some(source[key]) : Maybe<TValue>.None;
        }

        public static Maybe<TValue> Value<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key)
        {
            return source.ContainsKey(key) ? Some(source[key]) : Maybe<TValue>.None;
        }
    }
}