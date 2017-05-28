using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    public class Filter
    {
        #region Sync
        
        [Fact]
        public async Task Sync_OnSome_WithTruePredicate_ReturnsSome()
        {
            var expected = new {Flag = true};
            var isSome = await Some(expected).ToAsync().Filter(it => it.Flag).IsSome;
            Assert.True(isSome);
        }
        
        [Fact]
        public async Task Sync_OnSome_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = false};
            var isNone = await Some(expected).ToAsync().Filter(it => it.Flag).IsNone;
            Assert.True(isNone);
        }
        
        [Fact]
        public async Task Sync_OnNone_WithTruePredicate_ReturnsNone()
        {
            var expected = new {Flag = false};
            var isSome = await Some(expected).ToAsync().Filter(it => it.Flag).IsNone;
            Assert.True(isSome);
        }
        
        [Fact]
        public async Task Sync_OnNone_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = false};
            var isNone = await Some(expected).ToAsync().Filter(it => it.Flag).IsNone;
            Assert.True(isNone);
        }
        
        #endregion
        
        #region Async
        
        [Fact]
        public async Task Async_OnSome_WithTruePredicate_ReturnsSome()
        {
            var expected = new {Flag = Task.FromResult(true)};
            var isSome = await Some(expected).ToAsync().FilterAsync(async it => await it.Flag).IsSome;
            Assert.True(isSome);
        }
        
        [Fact]
        public async Task Async_OnSome_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = Task.FromResult(false)};
            var isNone = await Some(expected).ToAsync().FilterAsync(async it => await it.Flag).IsNone;
            Assert.True(isNone);
        }
        
        [Fact]
        public async Task Async_OnNone_WithTruePredicate_ReturnsNone()
        {
            var expected = new {Flag = Task.FromResult(false)};
            var isSome = await Some(expected).ToAsync().FilterAsync(async it => await it.Flag).IsNone;
            Assert.True(isSome);
        }
        
        [Fact]
        public async Task Async_OnNone_WithFalsePredicate_ReturnsNone()
        {
            var expected = new {Flag = Task.FromResult(false)};
            var isNone = await Some(expected).ToAsync().FilterAsync(async it => await it.Flag).IsNone;
            Assert.True(isNone);
        }
        
        #endregion
    }
}
