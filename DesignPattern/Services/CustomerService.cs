using System;
using System.Threading.Tasks;
using DesignPattern.Interface;

namespace DesignPattern.Services
{
    public class CustomerService : ICustomerService
    {
        public object Get(int id)
        {
            var data = new {id, name = "cook", age = 18, time = DateTime.Now.ToString("s")};
            return data;
        }

        public Task<string> GetAsync(int id)
        {
            return Task.FromResult(id.ToString());
        }

        public object NoAspect()
        {
            return new { time = DateTime.Now.ToString("s") };
        }
    }
}
