using System;

namespace Amplified.CSharp
{
    /// <summary>
    ///     None represents the lack of a value, alike <c>void</c>, but unlike <c>void</c>, <c>None</c> is a real type.
    /// </summary>
    /// <remarks>
    ///     This type has no internal fields, thus should theoretically take up 0 bytes on the stack, although there
    ///     may be some overhead associated with it. All <c>None</c>s are expectedly equal, and equality can be achieved
    ///     for other types by implementing <c>ICanBeNone</c> or <c>IMaybe</c>.
    /// </remarks>
    public struct None : IEquatable<None>, IEquatable<ICanBeNone>, ICanBeNone
    {
        public static readonly None Instance = default(None);
        public static readonly None _ = default(None);

        /// <summary>
        ///     Alawys returns <c>true</c>.
        /// </summary>
        public bool IsNone => true;

        public bool Equals(None other)
        {
            return true;
        }

        public bool Equals(ICanBeNone other)
        {
            return other?.IsNone ?? false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return (obj is None && Equals((None) obj)) ||
                   (obj is ICanBeNone && Equals((ICanBeNone) obj));
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(None left, None right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(None left, None right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(None left, ICanBeNone right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(None left, ICanBeNone right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return nameof(None);
        }
    }
}