
using System.Threading.Tasks;
using DesignPattern.Attributes;

namespace DesignPattern.Interface
{
    public interface ICustomerService : IScopeService
    {
        [Cache(Key = "test", ExpireMinutes = 1)]
        object Get(int id);

        [Cache(Key = "test-async")]
        Task<string> GetAsync(int id);

        object NoAspect();
    }
}
