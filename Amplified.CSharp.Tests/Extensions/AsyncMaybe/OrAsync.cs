using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_OrAsync
    {
        #region AsyncMaybe<T> OrAsync(Func<Task<AsyncMaybe<T>>> other)
        
        [Fact]
        public async Task OnSome_WithLambdaReturningAsyncSome_ReturnsSelf()
        {
            const int expected = 34;
            var source = await AsyncMaybe<int>.Some(expected).OrAsync(() => Task.FromResult(AsyncMaybe<int>.Some(545)));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task OnSome_WithLambdaReturningAsyncNone_ReturnsSelf()
        {
            const int expected = 34;
            
            // ReSharper disable once ConvertClosureToMethodGroup
            var source = await AsyncMaybe<int>.Some(expected).OrAsync(() => Task.FromResult(AsyncMaybe<int>.None()));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task OnSome_WithMethodReferenceReturningAsyncNone_ReturnsSelf()
        {
            const int expected = 34;
            Task<AsyncMaybe<int>> Other() => Task.FromResult(AsyncMaybe<int>.None());
            
            var source = await AsyncMaybe<int>.Some(expected).OrAsync(otherAsync: Other);
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task OnNone_WithLambdaReturningAsyncSome_ReturnsOther()
        {
            const int expected = 34;
            var source = await AsyncMaybe<int>.None().OrAsync(() => Task.FromResult(AsyncMaybe<int>.Some(expected)));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task OnNone_WithMethodReferenceReturningAsyncSome_ReturnsOther()
        {
            const int expected = 34;
            Task<AsyncMaybe<int>> Other() => Task.FromResult(AsyncMaybe<int>.Some(expected));
            
            var source = await AsyncMaybe<int>.None().OrAsync(otherAsync: Other);
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task OnNone_WithLambdaReturningAsyncNone_ReturnsNone()
        {
            // ReSharper disable once ConvertClosureToMethodGroup
            var source = await AsyncMaybe<int>.None().OrAsync(() => Task.FromResult(AsyncMaybe<int>.None()));
            source.MustBeNone();
        }

        [Fact]
        public async Task OnNone_WithMethodReferenceReturningAsyncNone_ReturnsNone()
        {
            Task<AsyncMaybe<int>> None() => Task.FromResult(AsyncMaybe<int>.None());
            var source = await AsyncMaybe<int>.None().OrAsync(otherAsync: None);
            source.MustBeNone();
        }
        
        #endregion
        
        #region AsyncMaybe<T> OrAsync(Task<AsyncMaybe<T>> other)
        
        [Fact]
        public async Task OnSome_WithAsyncSome_ReturnsSelf()
        {
            const int expected = 34;
            var source = await AsyncMaybe<int>.Some(expected).OrAsync(Task.FromResult(AsyncMaybe<int>.Some(545)));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task OnSome_WithAsyncNone_ReturnsSelf()
        {
            const int expected = 34;
            var source = await AsyncMaybe<int>.Some(expected).OrAsync(Task.FromResult(AsyncMaybe<int>.None()));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task OnNone_WithAsyncSome_ReturnsOther()
        {
            const int expected = 34;
            var source = await AsyncMaybe<int>.None().OrAsync(Task.FromResult(AsyncMaybe<int>.Some(expected)));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task OnNone_WithAsyncNone_ReturnsNone()
        {
            var source = await AsyncMaybe<int>.None().OrAsync(Task.FromResult(AsyncMaybe<int>.None()));
            source.MustBeNone();
        }
        
        #endregion
        
        #region AsyncMaybe<T> OrAsync(Func<Task<Maybe<T>>> other)
        
        [Fact]
        public async Task OnSome_WithLambdaReturningSome_ReturnsSelf()
        {
            const int expected = 34;
            var source = await AsyncMaybe<int>.Some(expected).OrAsync(() => Task.FromResult(Maybe<int>.Some(545)));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task OnSome_WithLambdaReturningNone_ReturnsSelf()
        {
            const int expected = 34;
            
            // ReSharper disable once ConvertClosureToMethodGroup
            var source = await AsyncMaybe<int>.Some(expected).OrAsync(() => Task.FromResult(Maybe<int>.None()));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task OnSome_WithMethodReferenceReturningNone_ReturnsSelf()
        {
            const int expected = 34;
            Task<Maybe<int>> Other() => Task.FromResult(Maybe<int>.None());
            
            var source = await AsyncMaybe<int>.Some(expected).OrAsync(other: Other);
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task OnNone_WithLambdaReturningSome_ReturnsOther()
        {
            const int expected = 34;
            var source = await AsyncMaybe<int>.None().OrAsync(() => Task.FromResult(Maybe<int>.Some(expected)));
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task OnNone_WithMethodReferenceReturningSome_ReturnsOther()
        {
            const int expected = 34;
            Task<Maybe<int>> Other() => Task.FromResult(Maybe<int>.Some(expected));
            
            var source = await AsyncMaybe<int>.None().OrAsync(other: Other);
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task OnNone_WithLambdaReturningNone_ReturnsNone()
        {
            // ReSharper disable once ConvertClosureToMethodGroup
            var source = await AsyncMaybe<int>.None().OrAsync(() => Task.FromResult(Maybe<int>.None()));
            source.MustBeNone();
        }

        [Fact]
        public async Task OnNone_WithMethodReferenceReturningNone_ReturnsNone()
        {
            Task<Maybe<int>> None() => Task.FromResult(Maybe<int>.None());
            var source = await AsyncMaybe<int>.None().OrAsync(other: None);
            source.MustBeNone();
        }
        
        #endregion
    }
}