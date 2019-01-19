using System.Threading.Tasks;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Units__StaticConstructors
    {
        [Fact]
        public void UnitConstructors()
        {
            var staticValue = Units.Unit();
            var defaultValue = default(Unit);
            var constructedValue = new Unit();
            var staticPropertyValue = Unit.Instance;
            
            Assert.Equal(staticValue, defaultValue);
            Assert.Equal(staticValue, constructedValue);
            Assert.Equal(staticValue, staticPropertyValue);
        }

        [Fact]
        public void WithLambdaAsAction_ReturnsFunc_Unit()
        {
            var result = Units.Unit(() => { });
            Assert.Equal(result, default(Unit));
        }

        [Fact]
        public void WithMethodReferenceAsAction_ReturnsFunc_Unit()
        {
            var output = string.Empty;
            void Foo() => output = "bar";
            var result = Units.Unit(Foo);
            Assert.Equal(result, default(Unit));
            Assert.Equal("bar", output);
        }

        [Fact]
        public async Task WithLambdaAsFunc_Task_ReturnsFunc_Task_Unit()
        {
            var result = await Units.UnitAsync(() => TaskCache.CompletedTask);
            Assert.Equal(result, default(Unit));
        }

        [Fact]
        public async Task WithAsyncLambdaAsFunc_Task_ReturnsFunc_Task_Unit()
        {
            var result = await Units.UnitAsync(async () => await TaskCache.CompletedTask);
            Assert.Equal(result, default(Unit));
        }

        [Fact]
        public async Task WithMethodReferenceAsFunc_Task_ReturnsFunc_Task_Unit()
        {
            Task Foo() => TaskCache.CompletedTask;
            var result = await Units.UnitAsync(Foo);
            Assert.Equal(result, default(Unit));
        }
    }
}