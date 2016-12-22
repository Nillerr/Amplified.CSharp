using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class MaybeToSomeExtensions
    {
        public static Some<T> OrSome<T>(this Maybe<T> source, [InstantHandle, NotNull] Func<T> some)
        {
            return source.Match(
                Some._,
                none => Some._(some())
            );
        }

        public static Some<T> OrSome<T>(this Maybe<T> source, T some)
        {
            return source.Match(
                value => value,
                none => some
            );
        }

        public static Maybe<Some<T>> AsSome<T>(this Maybe<T> source)
        {
            return source.Map(Some._);
        }
    }
}