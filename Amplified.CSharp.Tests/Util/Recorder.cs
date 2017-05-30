using System;
using Xunit;

namespace Amplified.CSharp.Util
{
    public struct Invocations
    {
        public Invocations(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public static class InvocationsFromInt
    {
        public static Invocations Invocations(this int value) => new Invocations(value);
    }
    
    public sealed class Recorder
    {
        public int Invocations { get; private set; }
        
        public Func<TResult> Record<TResult>(Func<TResult> func)
        {
            return () =>
            {
                Invocations++;
                return func();
            };
        }
        
        public Func<T, TResult> Record<T, TResult>(Func<T, TResult> func)
        {
            return arg =>
            {
                Invocations++;
                return func(arg);
            };
        }
        
        public Func<T1, T2, TResult> Record<T1, T2, TResult>(Func<T1, T2, TResult> func)
        {
            return (arg1, arg2) =>
            {
                Invocations++;
                return func(arg1, arg2);
            };
        }
        
        public Func<T1, T2, T3, TResult> Record<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func)
        {
            return (arg1, arg2, arg3) =>
            {
                Invocations++;
                return func(arg1, arg2, arg3);
            };
        }

        public void MustHaveExactly(Invocations invocations)
        {
            Assert.Equal(invocations.Value, Invocations);
        }
    }
}