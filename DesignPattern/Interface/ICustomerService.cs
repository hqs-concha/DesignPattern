
using Autofac.Extras.DynamicProxy;
using DesignPattern.Attributes;
using DesignPattern.Interceptor;

namespace DesignPattern.Interface
{
    [Intercept(typeof(CustomInterceptor))]
    public interface ICustomerService
    {
        [Cache("test")]
        object Get(int id);

        object NoAspect();
    }
}
