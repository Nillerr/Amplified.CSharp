using System;
using System.Diagnostics;

namespace Amplified.CSharp
{
    [DebuggerStepThrough]
    public struct Unit : IEquatable<Unit>
    {
        private static readonly int HashCode = nameof(Unit).GetHashCode();
        
        public static readonly Unit Instance = default(Unit);

        public TResult Match<TResult>(TResult result)
        {
            return result;
        }

        public TResult Match<TResult>(Func<TResult> result)
        {
            return result();
        }

        public TResult Match<TResult>(Func<Unit, TResult> result)
        {
            return result(this);
        }
        
        public bool Equals(Unit other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Unit;
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