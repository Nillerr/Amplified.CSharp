namespace Amplified.CSharp.Internal
{
    /// <summary>
    ///     Represents a type that can be equiavalent to <c>None</c>.
    /// </summary>
    public interface IMaybe
    {
        bool IsSome { get; }
        
        /// <summary>
        ///     When this property returns <c>true</c> for two objects, those objects should be considered equal.
        /// </summary>
        bool IsNone { get; }
    }
}