using System.Threading.Tasks;
using Orleans;

namespace Grains
{
    public class Hello : Grain, GrainInterfaces.IHello
    {
        public Task<string> SayHello(string greeting)
        {
            return Task.FromResult($"You say '{greeting}' and I say 'Hello!'");
        }
    }
}
