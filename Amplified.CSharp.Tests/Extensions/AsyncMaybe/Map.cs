using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_Map
    {
        [Fact]
        public async Task Sync_WhenSome_ReturnsMappedResult()
        {
            const int expected = 5;
            var result = await AsyncMaybe<int>.Some(2).Map(some => some + 3).OrFail();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Sync_WhenNone_ReturnsNone()
        {
            var isNone = await AsyncMaybe<int>.None().Map(some => some + 3).IsNone;
            Assert.True(isNone);
        }

        #region Map<T>(Action<T> mapper)

        [Fact]
        public async Task Some_Map_ActionLambda_ReturnsSomeUnit()
        {
            var source = AsyncMaybe<int>.Some(5);
            var result = await source.Map(it => { });
            var unit = result.MustBeSome();
            Assert.IsType<Unit>(unit);
        }

        [Fact]
        public async Task Some_Map_ActionMethodReference_ReturnsSomeUnit()
        {
            void Foo(int it) {}
            var source = AsyncMaybe<int>.Some(5);
            var result = await source.Map(Foo);
            var unit = result.MustBeSome();
            Assert.IsType<Unit>(unit);
        }

        [Fact]
        public async Task Some_Map_Action_ActionIsInvokedOnlyOnce()
        {
            var rec = new Recorder();
            var source = AsyncMaybe<int>.Some(5);
            var result = await source.Map(rec.Record((int it) => { }));
            result.MustBeSome();
            rec.MustHaveExactly(1.Invocations());
        }

        [Fact]
        public async Task None_Map_Action_ActionIsNotInvoked()
        {
            var rec = new Recorder();
            var source = AsyncMaybe<int>.None();
            var result = await source.Map(rec.Record((int it) => { }));
            result.MustBeNone();
            rec.MustHaveExactly(0.Invocations());
        }

        [Fact]
        public async Task None_Map_Action_ReturnsNoneUnit()
        {
            var source = AsyncMaybe<int>.None();
            var result = await source.Map(it => { });
            result.MustBeNone();
        }
        
        #endregion
    }
}
