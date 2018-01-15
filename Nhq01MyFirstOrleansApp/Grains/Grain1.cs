using System.Threading.Tasks;
using Nhq01MyFirstOrleansApp.GrainInterfaces;
using Orleans;

namespace Nhq01MyFirstOrleansApp.Grains
{
    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    public class Grain1 : Grain, IGrain1
    {
        public Task<string> SayHello()
        {
            return Task.FromResult("Hello world!");
        }
    }
}
