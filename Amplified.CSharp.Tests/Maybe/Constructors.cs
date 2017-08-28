using System;
using System.Threading.Tasks;
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
        public void UsingStaticGenericNone_OnMaybe()
        {
            Maybe<int> source = Maybe.None<int>();
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

        [Fact]
        public async Task SomeAsync_WithT_ReturnsAsyncMaybe()
        {
            const int expected = 3434;
            var result = Maybe.SomeAsync(expected);
            var maybe = await result;
            var value = maybe.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public void SomeAsync_WithNullTask_ThrowsException()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Maybe.SomeAsync<object>(null));
        }

        [Fact]
        public async Task SomeAsync_WithTaskWithNullResult_ThrowsException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await Maybe.SomeAsync(Task.FromResult<object>(null)));
        }

        [Fact]
        public void OfNullable_WithNonNull_ReturnsSome()
        {
            var expected = new object();
            var source = Maybe.OfNullable(expected);
            var value = source.MustBeSome();
            Assert.Same(expected, value);
        }

        [Fact]
        public void OfNullable_WithNull_ReturnsNone()
        {
            var source = Maybe.OfNullable<object>(null);
            source.MustBeNone();
        }

        [Fact]
        public async Task OfNullableAsync_WithNonNullReferenceType_ReturnsSome()
        {
            var expected = new object();
            var source = await Maybe.OfNullableAsync(Task.FromResult(expected));
            var value = source.MustBeSome();
            Assert.Same(expected, value);
        }

        [Fact]
        public async Task OfNullableAsync_WithNullReferenceType_ReturnsNone()
        {
            var source = await Maybe.OfNullableAsync(Task.FromResult<object>(null));
            source.MustBeNone();
        }

        [Fact]
        public async Task OfNullableAsync_WithNonNullValueType_ReturnsSome()
        {
            var expected = (int?) 3223;
            var source = await Maybe.OfNullableAsync(Task.FromResult(expected));
            var value = source.MustBeSome();
            Assert.Equal(expected, value);
        }

        [Fact]
        public async Task OfNullableAsync_WithNullValueType_ReturnsNone()
        {
            var source = await Maybe.OfNullableAsync(Task.FromResult<int?>(null));
            source.MustBeNone();
        }
    }
}
