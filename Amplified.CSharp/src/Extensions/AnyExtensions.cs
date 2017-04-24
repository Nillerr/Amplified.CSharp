namespace Amplified.CSharp.Extensions
{
    public static class AnyExtensions
    {
        public static Some<T> ToSome<T>(this T value)
        {
            return new Some<T>(value);
        }
    }
}