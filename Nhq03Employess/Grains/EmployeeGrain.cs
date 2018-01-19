using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;

namespace Grains
{
    public class EmployeeGrain : Grain, IEmployeeGrain
    {
        public Task<string> Hello()
        {
            return Task.FromResult("Hello man...");
        }
    }
}
