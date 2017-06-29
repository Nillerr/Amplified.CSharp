namespace Amplified.CSharp.Extensions
{
    public static class AnyToUnit
    {
        public static Unit ToUnit<T>(this T any)
        {
            return default(Unit);
        }
    }
}