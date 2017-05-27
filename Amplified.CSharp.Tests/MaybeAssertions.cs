using System;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    public static class MaybeAssertions
    {
        public static void MustBeNone<T>(this Maybe<T> source)
        {
            Assert.Equal(source.IsNone, true);
            Assert.Equal(source.IsSome, false);
        }

        public static void MustBeSome<T>(this Maybe<T> source)
        {
            Assert.Equal(source.IsNone, false);
            Assert.Equal(source.IsSome, true);
        }

        public static T OrFail<T>(this Maybe<T> source)
        {
            return source.OrThrow(() => new ArgumentException(nameof(source)));
        }

        public static async Task<T> OrFail<T>(this AsyncMaybe<T> source)
        {
            return (await source.ToTask()).OrFail();
        }
    }
}
