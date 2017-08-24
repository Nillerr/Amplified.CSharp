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

        public static Maybe<TSource> LastOrNone<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            var sourceList = source as IList<TSource>;
            if (sourceList != null)
            {
                var count = sourceList.Count;
                if (count > 0)
                    return Maybe<TSource>.Some(sourceList[count - 1]);
            }
            else
            {
                using (var enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        TSource current;
                        do
                        {
                            current = enumerator.Current;
                        }
                        while (enumerator.MoveNext());
                        return Maybe<TSource>.Some(current);
                    }
                }
            }
            return Maybe<TSource>.None();
        }

        public static Maybe<TSource> LastOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var matching = Maybe<TSource>.None();
            foreach (var element in source)
            {
                if (predicate(element))
                    matching = Maybe<TSource>.Some(element);
            }
            return matching;
        }

        public static Maybe<TSource> ElementAtOrNone<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            if (index >= 0)
            {
                var sourceList = source as IList<TSource>;
                if (sourceList != null)
                {
                    if (index < sourceList.Count)
                        return Maybe<TSource>.Some(sourceList[index]);
                }
                else
                {
                    foreach (var element in source)
                    {
                        if (index == 0)
                            return Maybe<TSource>.Some(element);
                        --index;
                    }
                }
            }
            return Maybe<TSource>.None();
        }

        public static Maybe<TValue> GetValueOrNone<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
        {
            return source.TryGetValue(key, out TValue value)
                ? Maybe<TValue>.Some(value)
                : Maybe<TValue>.None();
        }

        public static Maybe<TValue> GetValueOrNone<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key)
        {
            return source.TryGetValue(key, out TValue value)
                ? Maybe<TValue>.Some(value)
                : Maybe<TValue>.None();
        }
    }
}