using System;
using JetBrains.Annotations;

namespace Amplified.CSharp.Extensions
{
    public static class EitherExtensions
    {
        public static Either<TLeftResult, TRightResult> Map<TLeft, TRight, TLeftResult, TRightResult>(
            this Either<TLeft, TRight> source,
            [InstantHandle, NotNull] Func<TLeft, TLeftResult> leftMapper,
            [InstantHandle, NotNull] Func<TRight, TRightResult> rightMapper
        )
        {
            return source.Match(
                left => Either.Left<TLeftResult, TRightResult>(leftMapper(left)),
                right => Either.Right<TLeftResult, TRightResult>(rightMapper(right))
            );
        }

        public static Either<TResult, TRight> MapLeft<TLeft, TRight, TResult>(
            this Either<TLeft, TRight> source,
            [InstantHandle, NotNull] Func<TLeft, TResult> mapper
        )
        {
            return source.Map(mapper, right => right);
        }

        public static Either<TLeft, TResult> MapRight<TLeft, TRight, TResult>(
            this Either<TLeft, TRight> source,
            [InstantHandle, NotNull] Func<TRight, TResult> mapper
        )
        {
            return source.Map(left => left, mapper);
        }

        public static TResult Cast<TLeft, TRight, TResult>(this Either<TLeft, TRight> source, Type<TResult> type)
            where TLeft : TResult
            where TRight : TResult
        {
            return source.Match(
                left => (TResult) left,
                right => (TResult) right
            );
        }
    }
}