using System.Threading.Tasks;

namespace Amplified.CSharp.Internal
{
    public interface IAsyncCanBeNone
    {
        Task<bool> IsNoneAsync { get; }
    }
}