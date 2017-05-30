using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_Where
    {
        #region Sync
        
        [Fact]
        public async Task Sync_OnSome_WithTruePredicate_ReturnsSome()
        {
            var expected = new {Flag = true};
            var source = await SomeAsync(expected).Where(it => it.Flag);
            source.MustBeSome();
        }
        
        [Fact]
        public async Task Sync_OnSome_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = false};
            var source = await SomeAsync(expected).Where(it => it.Flag);
            source.MustBeNone();
        }
        
        [Fact]
        public async Task Sync_OnNone_WithTruePredicate_ReturnsNone()
        {
            var expected = new {Flag = false};
            var source = await SomeAsync(expected).Where(it => it.Flag);
            source.MustBeNone();
        }
        
        [Fact]
        public async Task Sync_OnNone_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = false};
            var source = await SomeAsync(expected).Where(it => it.Flag);
            source.MustBeNone();
        }
        
        #endregion
        
        #region Async
        
        [Fact]
        public async Task Async_OnSome_WithTruePredicate_ReturnsSome()
        {
            var expected = new {Flag = Task.FromResult(true)};
            var source = await SomeAsync(expected).WhereAsync(async it => await it.Flag);
            source.MustBeSome();
        }
        
        [Fact]
        public async Task Async_OnSome_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = Task.FromResult(false)};
            var source = await SomeAsync(expected).WhereAsync(async it => await it.Flag);            
            source.MustBeNone();
        }
        
        [Fact]
        public async Task Async_OnNone_WithTruePredicate_ReturnsNone()
        {
            var invocations = 0;
            var source = await AsyncMaybe<object>.None()
                .WhereAsync(async it => { invocations++; return await Task.FromResult(true); });
            
            source.MustBeNone();
            Assert.Equal(0, invocations);
        }
        
        [Fact]
        public async Task Async_OnNone_WithFalsePredicate_ReturnsNone()
        {
            var invocations = 0;
            var source = await AsyncMaybe<object>.None()
                .WhereAsync(async it => { invocations++; return await Task.FromResult(false); });
            source.MustBeNone();
            Assert.Equal(0, invocations);
        }
        
        #endregion
    }
}
