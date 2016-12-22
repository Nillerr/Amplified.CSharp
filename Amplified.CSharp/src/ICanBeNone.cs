namespace Amplified.CSharp
{
    /// <summary>
    ///     Represents a type that can be equiavalent to <c>None</c>.
    /// </summary>
    public interface ICanBeNone
    {
        bool IsNone { get; }
    }
}