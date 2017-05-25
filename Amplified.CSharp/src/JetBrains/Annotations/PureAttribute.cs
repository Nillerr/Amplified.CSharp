using System;

namespace JetBrains.Annotations
{
    /// <summary>
    /// Indicates that a method does not make any observable state changes. The same as <see cref="System.Diagnostics.Contracts.PureAttribute"/>.
    /// </summary>
    /// <example><code>
    /// [Pure] int Multiply(int x, int y) => x * y;
    /// 
    /// void M() {
    ///     Multiply(123, 42); // Waring: Return value of pure method is not used
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate | AttributeTargets.GenericParameter)]
    internal sealed class PureAttribute : Attribute
    {
    }
}