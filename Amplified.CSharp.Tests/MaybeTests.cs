using System;
using Xunit;
using static Amplified.CSharp.Constructors;

namespace Amplified.CSharp
{
    public class MaybeTests
    {
        [Fact]
        public void ImplicitFromNone()
        {
            Maybe<int> converted = default(None);
            converted.MustBeNone();
        }

        [Fact]
        public void DefaultValue_IsNone()
        {
            var maybe = default(Maybe<object>);
            maybe.MustBeNone();
        }

        [Fact]
        public void CallingEmptyConstructor_ReturnsNone()
        {
            var maybe = new Maybe<object>();
            maybe.MustBeNone();
        }

        [Fact]
        public void CallingConstructor_WithValue_ReturnsSome()
        {
            var maybe = new Maybe<object>(new object());
            maybe.MustBeSome();
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
            var some = Some(1);
            some.MustBeSome();
        }

        [Fact]
        public void StaticNone_ReturnsNone()
        {
            var none = Maybe<int>.None;
            none.MustBeNone();
        }

        [Fact]
        public void StaticNoneMethod_ReturnsNone()
        {
            var none = Maybe<int>.None;
            none.MustBeNone();
        }

        [Fact]
        public void StaticSome_WithNullArgument_ThrowsException()
        {
            object obj = null;
            
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Some(obj));
        }

        [Fact]
        public void Match_OnSome_ReturnsValue()
        {
            var expected = new object();
            var result = Some(1)
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
            var result = Maybe<int>.None
                .Match(
                    some => new object(),
                    none => expected
                );
            
            Assert.Same(expected, result);
        }
    }
}