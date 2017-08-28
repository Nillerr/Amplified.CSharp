using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_Select
    {
        #region Select<T, TResult>(Func<T, TResult> selector) 
        
        [Fact]
        public async Task Sync_OnSome_ReturnsSameAsMap()
        {
            var source = AsyncMaybe<int>.Some(3);
            var where = await source.Select(it => it * 2);
            var map = await source.Map(it => it * 2);
            Assert.Equal(where, map);
        }
        
        [Fact]
        public async Task Sync_OnNone_returnsSameAsMap()
        {
            var source = AsyncMaybe<int>.None();
            var where = await source.Select(it => it * 2);
            var map = await source.Map(it => it * 2);
            Assert.Equal(where, map);
        }
        
        #endregion
        
        #region SelectAsync<T, TResult>(Func<T, Task<TResult>> selector)

        [Fact]
        public async Task Async_OnSome_ReturnsSameAsMapAsync()
        {
            var source = AsyncMaybe<int>.Some(3);
            var where = await source.SelectAsync(it => Task.FromResult(it * 2));
            var map = await source.MapAsync(it => Task.FromResult(it * 2));
            Assert.Equal(where, map);
        }

        [Fact]
        public async Task Async_OnNone_ReturnsSameAsMapAsync()
        {
            var source = AsyncMaybe<int>.None();
            var where = await source.SelectAsync(it => Task.FromResult(it * 2));
            var map = await source.MapAsync(it => Task.FromResult(it * 2));
            Assert.Equal(where, map);
        }
        
        #endregion
    }
}