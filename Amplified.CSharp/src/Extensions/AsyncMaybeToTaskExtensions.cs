using System.Threading.Tasks;
using static Amplified.CSharp.Maybe;

namespace Amplified.CSharp.Extensions
{
    public static class AsyncMaybeToTaskExtensions
    {
        public static Task<Maybe<T>> ToTask<T>(this AsyncMaybe<T> source)
        {
            return source.Match(some: Some, none: none => Maybe<T>.None);
        }
    }
}
