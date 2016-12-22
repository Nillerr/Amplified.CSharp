namespace Amplified.CSharp
{
    /// <summary>
    ///     Type token used to aid type inference in certain cases, such as Some.ToLeft, Some.ToRight, Some.Left,
    ///     Some.Right.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Type<T>
    {
        public static Type<T> _ = default(Type<T>);
    }
}