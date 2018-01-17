using System.Threading.Tasks;

namespace Grains
{
    public class Hello : GrainInterfaces.IHello
    {
        public Task<string> SayHello(string greeting)
        {
            return Task.FromResult($"You say '{greeting}' and I say 'Hello!'");
        }
    }
}
