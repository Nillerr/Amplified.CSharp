using System;
using System.Diagnostics;

namespace Amplified.CSharp
{
    public abstract class EnumsBase<TEnum>
    {
        internal EnumsBase()
        {
        }

        [DebuggerStepThrough]
        public static Maybe<T> Parse<T>(string str)
            where T : struct, TEnum
            => Enum.TryParse(str, out T value)
                ? Maybe<T>.Some(value)
                : Maybe<T>.None();   
    }
    
    public sealed class Enums : EnumsBase<Enum>
    {
        private Enums()
        {
        }
    }
}