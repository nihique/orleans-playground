using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IEmployeeGrain : IGrainWithGuidKey
    {
        Task<string> Hello();
    }
}
