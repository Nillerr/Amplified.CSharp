using System;
using System.Threading.Tasks;

namespace Amplified.CSharp
{
    public static class Units
    {
        public static Unit Unit() => default(Unit);
        
        public static Unit Unit(Action action)
        {
            action();
            return default(Unit);
        }
        
        public static async Task<Unit> Unit(Func<Task> action)
        {
            await action();
            return default(Unit);
        }
        
        public static Unit Unit<T>(Func<T> action)
        {
            /*var ignored = */action();
            return default(Unit);
        }
    }
}