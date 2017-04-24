using System;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    public class MaybeTests
    {
        [Fact]
        public void DefaultValue_IsNone()
        {
            var maybe = default(Maybe<object>);
            maybe.AssertIsNone();
        }

        [Fact]
        public void CallingEmptyConstructor_ReturnsNone()
        {
            var maybe = new Maybe<object>();
            maybe.AssertIsNone();
        }

        [Fact]
        public void CallingConstructor_WithValue_ReturnsSome()
        {
            var maybe = new Maybe<object>(new object());
            maybe.AssertIsSome();
        }

        [Fact]
        public void CallingConstructor_WithNullReferenceType_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Maybe<object>(null));
        }

        [Fact]
        public void CallingConstructor_WithNullValueType_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Maybe<int?>(null));
        }

        [Fact]
        public void StaticSome_WithNonNullArgument_ReturnsSome()
        {
            var some = Maybe.Some(1);
            some.AssertIsSome();
        }

        [Fact]
        public void StaticNone_ReturnsNone()
        {
            var none = Maybe<int>.None;
            none.AssertIsNone();
        }

        [Fact]
        public void StaticNoneMethod_ReturnsNone()
        {
            var none = Maybe.None<int>();
            none.AssertIsNone();
        }

        [Fact]
        public void StaticSome_WithNullArgument_ThrowsException()
        {
            object obj = null;
            Assert.Throws<ArgumentNullException>(() => Maybe.Some(obj));
        }

        [Fact]
        public void Match_OnSome_ReturnsValue()
        {
            var expected = new object();
            var result = Maybe.Some(1)
                .Match(
                    some => expected,
                    none => new object()
                );
            Assert.Same(expected, result);
        }

        [Fact]
        public void Match_OnNone_ReturnsValue()
        {
            var expected = new object();
            var result = Maybe.None<int>()
                .Match(
                    some => new object(),
                    none => expected
                );
            Assert.Same(expected, result);
        }

        [Fact]
        public void MatchSome_OnSome_ReturnsValue()
        {
            var expected = Some._(43);
            var result = Maybe.Some(1)
                .Match(
                    some => expected.Value,
                    none => 2
                )
                .ToSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Match_WithSingleSomeAction_OnSome_InvokesActionWithValue()
        {
            const int expected = 945;
            var actual = 0;
            Maybe.Some(expected).Match(some => actual = some);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Match_WithSingleNoneAction_OnSome_DoesNotInvokeNoneAction()
        {
            var invoked = false;
            Maybe.Some(new object()).Match(none: () => invoked = true);
            Assert.False(invoked);
        }

        [Fact]
        public void Match_WithBothAction_OnSome_InvokesSomeAction()
        {
            var expected = new object();
            object someValue = null;

            var noneInvoked = false;
            Maybe.Some(expected)
                .Match(
                    some => someValue = some,
                    none: () => noneInvoked = true
                );

            Assert.Same(expected, someValue);
            Assert.False(noneInvoked);
        }

        [Fact]
        public void Match_WithSingleSomeAction_OnNone_DoesNotInvokeSomeAction()
        {
            var invoked = false;
            Maybe.None<object>().Match(some => invoked = true);
            Assert.False(invoked);
        }

        [Fact]
        public void Match_WithSingleNoneAction_OnNone_InvokesNoneAction()
        {
            var invoked = false;
            Maybe.None<object>().Match(none: () => invoked = true);
            Assert.True(invoked);
        }

        [Fact]
        public void Match_WithBothActions_OnNone_InvokesNoneAction()
        {
            var noneInvoked = false;
            Maybe.None<object>().Match(some => { }, none: () => noneInvoked = true);
            Assert.True(noneInvoked);
        }

        [Fact]
        public void Match_WithBothActions_OnNone_DoesNotInvokesSomeAction()
        {
            var someInvoked = false;
            Maybe.None<object>().Match(some => someInvoked = true, none: () => { });
            Assert.False(someInvoked);
        }

    }
}