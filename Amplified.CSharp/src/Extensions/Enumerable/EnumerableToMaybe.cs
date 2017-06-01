using System;
using System.Collections.Generic;

namespace Amplified.CSharp.Extensions
{
    public static class EnumerableToMaybe
    {
        public static Maybe<TSource> SingleOrNone<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            var list = source as IList<TSource>;
            if (list != null)
            {
                switch (list.Count)
                {
                    case 0:
                        return Maybe<TSource>.None();
                    case 1:
                        return Maybe<TSource>.Some(list[0]);
                }
            }
            else
            {
                using (var enumerator = source.GetEnumerator())
                {
                    if (!enumerator.MoveNext())
                        return Maybe<TSource>.None();

                    var current = enumerator.Current;
                    if (!enumerator.MoveNext())
                        return Maybe<TSource>.Some(current);
                }
            }
            
            throw new InvalidOperationException("The sequence contains more than one element.");
        }
        
        public static Maybe<TSource> SingleOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    if (!predicate(current))
                        continue;
                    
                    while (enumerator.MoveNext())
                    {
                        if (predicate(enumerator.Current))
                            throw new InvalidOperationException("The sequence contains more than one element matching the predicate.");
                    }
                    return Maybe<TSource>.Some(current);
                }
            }
            
            return Maybe<TSource>.None();
        }

        public static Maybe<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            var sourceList = source as IList<TSource>;
            if (sourceList != null)
            {
                if (sourceList.Count > 0)
                    return Maybe<TSource>.Some(sourceList[0]);
            }
            else
            {
                using (var enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                        return Maybe<TSource>.Some(enumerator.Current);
                }
            }
            return Maybe<TSource>.None();
        }

        public static Maybe<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            
            foreach (var element in source)
            {
                if (predicate(element))
                    return Maybe<TSource>.Some(element);
            }
            return Maybe<TSource>.None();
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
                        return Maybe<T>.Some(list[count - 1]);
                    }
                }

                var last = Maybe<T>.None();
                while (enumerator.MoveNext())
                {
                    last = Maybe<T>.Some(enumerator.Current);
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
                    return Maybe<T>.Some(list[index]);
                }
            }
            else
            {
                using (var enumerator = source.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext() == false) return Maybe<T>.None();
                        if (index == 0) return Maybe<T>.Some(enumerator.Current);
                        index--;
                    }
                }
            }
            return Maybe<T>.None();
        }

        public static Maybe<TValue> GetValueOrNone<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
        {
            return source.ContainsKey(key) ? Maybe<TValue>.Some(source[key]) : Maybe<TValue>.None();
        }

        public static Maybe<TValue> GetValueOrNone<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key)
        {
            return source.ContainsKey(key) ? Maybe<TValue>.Some(source[key]) : Maybe<TValue>.None();
        }
    }
}