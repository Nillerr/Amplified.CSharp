using System;

namespace Amplified.CSharp.Extensions
{
    public static class ActionToFuncUnit
    {
        public static Func<T, Unit> WithUnitResult<T>(this Action<T> action)
        {
            return arg =>
            {
                action(arg);
                return default(Unit);
            };
        }
    }
}