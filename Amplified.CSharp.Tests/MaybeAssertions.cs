﻿using System;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    public static class MaybeAssertions
    {
        public static void AssertIsNone<T>(this Maybe<T> source)
        {
            Assert.Equal(source.IsNone, true);
            Assert.Equal(source.IsSome, false);
        }

        public static void AssertIsSome<T>(this Maybe<T> source)
        {
            Assert.Equal(source.IsNone, false);
            Assert.Equal(source.IsSome, true);
        }

        public static T OrFail<T>(this Maybe<T> source)
        {
            return source.OrThrow(() => new ArgumentException(nameof(source)));
        }
    }
}