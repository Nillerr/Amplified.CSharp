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

        public Action<T> Record<T>(Action<T> action) => arg =>
        {
            Invocations++;
            action(arg);
        };

        public Func<TResult> Record<TResult>(Func<TResult> func) => () =>
        {
            Invocations++;
            return func();
        };

        public Func<T, TResult> Record<T, TResult>(Func<T, TResult> func) => arg =>
        {
            Invocations++;
            return func(arg);
        };

        public Func<T1, T2, TResult> Record<T1, T2, TResult>(Func<T1, T2, TResult> func) => (arg1, arg2) =>
        {
            Invocations++;
            return func(arg1, arg2);
        };

        public Func<T1, T2, T3, TResult> Record<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func) => (arg1, arg2, arg3) =>
        {
            Invocations++;
            return func(arg1, arg2, arg3);
        };

        public void MustHaveExactly(Invocations invocations)
        {
            Assert.Equal(invocations.Value, Invocations);
        }
    }
}