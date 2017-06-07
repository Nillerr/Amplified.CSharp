﻿using System.Collections.Generic;

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
                        return Some._(list[0]);
                    }
                }
                else
                {
                    if (enumerator.MoveNext())
                    {
                        var result = enumerator.Current;
                        if (enumerator.MoveNext() == false)
                        {
                            return Some._(result);
                        }
                    }
                }
                return None._;
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
                        return Some._(list[0]);
                    }
                }
                else if (enumerator.MoveNext())
                {
                    return Some._(enumerator.Current);
                }
                return None._;
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
                        return Some._(list[count - 1]);
                    }
                }

                var last = Maybe<T>.None;
                while (enumerator.MoveNext())
                {
                    last = Some._(enumerator.Current);
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
                    return Some._(list[index]);
                }
            }
            else
            {
                using (var enumerator = source.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext() == false) return None._;
                        if (index == 0) return Some._(enumerator.Current);
                        index--;
                    }
                }
            }
            return None._;
        }

        public static Maybe<TValue> Value<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
        {
            return source.ContainsKey(key) ? Maybe.Some(source[key]) : Maybe<TValue>.None;
        }

        public static Maybe<TValue> Value<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key)
        {
            return source.ContainsKey(key) ? Maybe.Some(source[key]) : Maybe<TValue>.None;
        }
    }
}