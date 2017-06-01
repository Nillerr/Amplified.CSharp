using System;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_OrThrowAsync
    {
        [Fact]
        public async Task Sync_OnSome_WithFunction_ReturnsResultOfSome()
        {
            const int expected = 123132;
            var source = Maybe.Some(expected).ToAsync();
            var result = await source.OrThrowAsync(() => Task.FromResult<Exception>(new ExpectedException()));
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Sync_OnNone_WithFunction_ThrowsException()
        {
            var source = AsyncMaybe<int>.None();
            await Assert.ThrowsAsync<ExpectedException>(() => source.OrThrowAsync(() => Task.FromResult<Exception>(new ExpectedException())));
        }
        
        [Fact]
        public async Task Async_OnSome_WithFunction_ReturnsResultOfSome()
        {
            const int expected = 123132;
            var source = Maybe.Some(expected).ToAsync();
            var result = await source.OrThrowAsync(() => Task.FromResult<Exception>(new ExpectedException()));
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task Async_OnNone_WithFunction_ThrowsException()
        {
            var source = AsyncMaybe<int>.None();
            await Assert.ThrowsAsync<ExpectedException>(() => source.OrThrowAsync(() => Task.FromResult<Exception>(new ExpectedException())));
        }
    }
}