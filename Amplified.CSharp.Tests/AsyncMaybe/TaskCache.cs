using System.Threading.Tasks;

namespace Amplified.CSharp
{
    internal static class TaskCache
    {
        internal static readonly Task CompletedTask = Task.FromResult<object>(null);
    }
}