using System;

namespace Amplified.CSharp.Extensions
{
    public static class UnitToAny
    {
        public static TResult Return<TResult>(this Unit unit, TResult result)
        {
            return unit.Match(result);
        }
        
        public static TResult Get<TResult>(this Unit unit, Func<TResult> result)
        {
            return unit.Match(result);
        }
    }
}