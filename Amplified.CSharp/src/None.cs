using System;
using System.Diagnostics;
using Amplified.CSharp.Internal;
using JetBrains.Annotations;

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
    [DebuggerStepThrough]
    public struct None : IEquatable<None>, IEquatable<IMaybe>
    {
        [Pure]
        public TResult Match<TResult>([InstantHandle, NotNull] Func<None, TResult> none)
        {
            return none(this);
        }
        
        [Pure]
        public TResult Match<TResult>([InstantHandle, NotNull] Func<TResult> none)
        {
            return none();
        }
        
        public void Match([InstantHandle, NotNull] Action<None> none)
        {
            none(this);
        }
        
        public void Match([InstantHandle, NotNull] Action none)
        {
            none();
        }

        [Pure]
        public bool Equals(None other)
        {
            return true;
        }

        [Pure]
        public bool Equals(IMaybe other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            
            return other.IsNone;
        }

        [Pure]
        public bool Equals<T>(Maybe<T> other)
        {
            return other.IsNone;
        }

        [Pure]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return (obj is None && Equals((None) obj)) ||
                   (obj is IMaybe && Equals((IMaybe) obj));
        }

        [Pure]
        public override int GetHashCode()
        {
            return 0;
        }

        [Pure]
        public static bool operator ==(None left, None right)
        {
            return left.Equals(right);
        }

        [Pure]
        public static bool operator !=(None left, None right)
        {
            return !left.Equals(right);
        }
        
        [Pure]
        public override string ToString()
        {
            return nameof(None);
        }
    }
}