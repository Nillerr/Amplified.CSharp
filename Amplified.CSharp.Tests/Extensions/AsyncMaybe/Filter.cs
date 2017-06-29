using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_Filter
    {
        #region Sync
        
        [Fact]
        public async Task Sync_OnSome_WithTruePredicate_ReturnsSome()
        {
            var expected = new {Flag = true};
            var source = await SomeAsync(expected).Filter(it => it.Flag);
            var result = source.MustBeSome();
            Assert.Same(expected, result);
        }
        
        [Fact]
        public async Task Sync_OnSome_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = false};
            var source = await SomeAsync(expected).Filter(it => it.Flag);
            source.MustBeNone();
        }
        
        [Fact]
        public async Task Sync_OnNone_WithTruePredicate_ReturnsNone()
        {
            var source = await AsyncMaybe<object>.None().Filter(it => true);
            source.MustBeNone();
        }
        
        [Fact]
        public async Task Sync_OnNone_WithFalsePredicate_ReturnsNone()
        {
            var source = await AsyncMaybe<object>.None().Filter(it => false);
            source.MustBeNone();
        }
        
        #endregion
        
        #region Async
        
        [Fact]
        public async Task Async_OnSome_WithTruePredicate_ReturnsSome()
        {
            var expected = new {Flag = Task.FromResult(true)};
            var source = await SomeAsync(expected).FilterAsync(async it => await it.Flag);
            var result = source.MustBeSome();
            Assert.Same(expected, result);
        }
        
        [Fact]
        public async Task Async_OnSome_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = Task.FromResult(false)};
            var source = await SomeAsync(expected).FilterAsync(async it => await it.Flag);            
            source.MustBeNone();
        }
        
        [Fact]
        public async Task Async_OnNone_WithTruePredicate_ReturnsNone()
        {
            var rec = new Recorder();
            var source = await AsyncMaybe<object>.None()
                .FilterAsync(rec.Record(async (object it) => await Task.FromResult(true)));
            
            source.MustBeNone();
            rec.MustHaveExactly(0.Invocations());
        }
        
        [Fact]
        public async Task Async_OnNone_WithFalsePredicate_ReturnsNone()
        {
            var rec = new Recorder();
            var source = await AsyncMaybe<object>.None()
                .FilterAsync(rec.Record(async (object it) => await Task.FromResult(false)));
            
            source.MustBeNone();
            rec.MustHaveExactly(0.Invocations());
        }
        
        #endregion
    }
}
