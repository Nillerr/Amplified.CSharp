using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class AsyncMaybe_Flatten
    {
        [Fact]
        public async Task WithSomeSome_ReturnsResultOfSome()
        {
            const int expected = 3;
            var source = await AsyncMaybe<int>.Some(1).Map(it => Maybe<int>.Some(3)).Flatten();
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task WithAsyncSomeSome_ReturnsResultOfSome()
        {
            const int expected = 3;
            var source = await AsyncMaybe<int>.Some(1).Map(it => AsyncMaybe<int>.Some(3)).Flatten();
            var result = source.MustBeSome();
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task WithSomeNone_ReturnsNone()
        {
            var source = await AsyncMaybe<int>.Some(1).Map(it => Maybe<int>.None()).Flatten();
            source.MustBeNone();
        }
        
        [Fact]
        public async Task WithAsyncSomeNone_ReturnsNone()
        {
            var source = await AsyncMaybe<int>.Some(1).Map(it => AsyncMaybe<int>.None()).Flatten();
            source.MustBeNone();
        }
        
        [Fact]
        public async Task WithNoneSome_ReturnsNone()
        {
            var source = await AsyncMaybe<int>.None().Map(it => Maybe<int>.Some(1)).Flatten();
            source.MustBeNone();
        }
        
        [Fact]
        public async Task WithAsyncNoneSome_ReturnsNone()
        {
            var source = await AsyncMaybe<int>.None().Map(it => AsyncMaybe<int>.Some(1)).Flatten();
            source.MustBeNone();
        }
        
        [Fact]
        public async Task WithNoneNone_ReturnsNone()
        {
            var source = await AsyncMaybe<int>.None().Map(it => Maybe<int>.None()).Flatten();
            source.MustBeNone();
        }
        
        [Fact]
        public async Task WithAsyncNoneNone_ReturnsNone()
        {
            var source = await AsyncMaybe<int>.None().Map(it => AsyncMaybe<int>.None()).Flatten();
            source.MustBeNone();
        }
    }
}
