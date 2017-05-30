using System;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Maybe__Constructors
    {
        [Fact]
        public void ImplicitFromNone()
        {
            Maybe<int> source = new None();
            source.MustBeNone();
        }

        [Fact]
        public void UsingEmptyConstructor()
        {
            var source = new Maybe<int>();
            source.MustBeNone();
        }

        [Fact]
        public void UsingStaticSome_WithNullValue_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Maybe<object>.Some(null));
        }

        [Fact]
        public void UsingStaticSome_OnGenericMaybe()
        {
            Maybe<int> source = Maybe<int>.Some(1);
            var value = source.OrFail();
            Assert.Equal(1, value);
        }

        [Fact]
        public void UsingStaticNone_OnGenericMaybe()
        {
            Maybe<int> source = Maybe<int>.None();
            source.MustBeNone();
        }

        [Fact]
        public void UsingStaticSome_OnMaybe()
        {
            Maybe<int> source = Maybe.Some(1);
            var value = source.OrFail();
            Assert.Equal(1, value);
        }

        [Fact]
        public void UsingStaticNone_OnMaybe()
        {
            Maybe<int> source = Maybe.None();
            source.MustBeNone();
        }

        [Fact]
        public void UsingStaticOfNullable_WithNonNullValueType_OnMaybe()
        {
            var expected = (int?) 1;
            Maybe<int> source = Maybe.OfNullable(expected);
            var value = source.OrFail();
            Assert.Equal(expected.Value, value);
        }

        [Fact]
        public void UsingStaticOfNullable_WithNullValueType_OnMaybe()
        {
            var expected = (int?) null;
            Maybe<int> source = Maybe.OfNullable(expected);
            source.MustBeNone();
        }
    }
}
