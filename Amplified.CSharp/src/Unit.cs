using System;

namespace Amplified.CSharp
{
    public struct Unit : IEquatable<Unit>
    {
        private static readonly int HashCode = nameof(Unit).GetHashCode();
        
        public static readonly Unit Instance = default(Unit);
        
        public bool Equals(Unit other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Unit && Equals((Unit) obj);
        }

        public override int GetHashCode()
        {
            return HashCode;
        }

        public static bool operator ==(Unit left, Unit right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Unit left, Unit right)
        {
            return !left.Equals(right);
        }
    }
}