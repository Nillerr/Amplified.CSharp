using System;
using System.Globalization;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp
{
    public static class Functions
    {
        public static Maybe<byte> ParseByte(string str)
        {
            return byte.TryParse(str, out byte result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<byte> ParseByte(string str, NumberStyles style, IFormatProvider provider)
        {
            return byte.TryParse(str, style, provider, out byte result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<short> ParseShort(string str)
        {
            return short.TryParse(str, out short result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<short> ParseShort(string str, NumberStyles style, IFormatProvider provider)
        {
            return short.TryParse(str, style, provider, out short result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<ushort> ParseUnsignedShort(string str)
        {
            return ushort.TryParse(str, out ushort result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<ushort> ParseUnsignedShort(string str, NumberStyles style, IFormatProvider provider)
        {
            return ushort.TryParse(str, style, provider, out ushort result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<char> ParseChar(string str)
        {
            return char.TryParse(str, out char result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<int> ParseInt(string str)
        {
            return int.TryParse(str, out int result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<int> ParseInt(string str, NumberStyles style, IFormatProvider provider)
        {
            return int.TryParse(str, style, provider, out int result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<uint> ParseUnsignedInt(string str)
        {
            return uint.TryParse(str, out uint result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<uint> ParseUnsignedInt(string str, NumberStyles style, IFormatProvider provider)
        {
            return uint.TryParse(str, style, provider, out uint result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<long> ParseLong(string str)
        {
            return long.TryParse(str, out long result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<long> ParseLong(string str, NumberStyles style, IFormatProvider provider)
        {
            return long.TryParse(str, style, provider, out long result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<ulong> ParseUnsignedLong(string str)
        {
            return ulong.TryParse(str, out ulong result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<ulong> ParseUnsignedLong(string str, NumberStyles style, IFormatProvider provider)
        {
            return ulong.TryParse(str, style, provider, out ulong result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<double> ParseDouble(string str)
        {
            return double.TryParse(str, out double result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<double> ParseDouble(string str, NumberStyles style, IFormatProvider provider)
        {
            return double.TryParse(str, style, provider, out double result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<float> ParseFloat(string str)
        {
            return float.TryParse(str, out float result)
                ? Some(result)
                : None();
        }
        
        public static Maybe<float> ParseFloat(string str, NumberStyles style, IFormatProvider provider)
        {
            return float.TryParse(str, style, provider, out float result)
                ? Some(result)
                : None();
        }
    }
}