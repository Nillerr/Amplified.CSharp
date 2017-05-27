using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Amplified.CSharp
{
    public static class Constructors
    {
        public static Unit Unit() => default(Unit);
        
        public static Unit Unit(Action action)
        {
            action();
            return default(Unit);
        }
        
        public static async Task<Unit> Unit(Func<Task> action)
        {
            await action();
            return default(Unit);
        }
        
        public static Unit Unit<T>(Func<T> action)
        {
            /*var ignored = */action();
            return default(Unit);
        }

        public static Maybe<T> Some<T>([NotNull] T value) => new Maybe<T>(value);

        public static None None() => default(None);

        public static Maybe<T> OfNullable<T>([CanBeNull] T value) where T : class
        {
            return value == null ? Maybe<T>.None : new Maybe<T>(value);
        }

        public static Maybe<T> OfNullable<T>([CanBeNull] T? value) where T : struct
        {
            return value.HasValue ? new Maybe<T>(value.Value) : Maybe<T>.None;
        }
    }
}
