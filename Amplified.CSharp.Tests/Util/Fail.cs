using System;

namespace Amplified.CSharp.Util
{
    public static class Fail
    {
        public static T With<T>(T value)
        {
            throw new InvalidOperationException("Operation was not suppoed to be invoked.");
        }

        public static T With<T>()
        {
            throw new InvalidOperationException("Operation was not suppoed to be invoked.");
        }
    }
}