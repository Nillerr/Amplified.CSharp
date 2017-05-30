using System;
using System.Threading.Tasks;
using Amplified.CSharp.Internal.Extensions;

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

        public static Task<Unit> UnitAsync(Func<Task> async) => async().WithResult(default(Unit));
    }
}