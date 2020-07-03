using System;
using Castle.DynamicProxy;

namespace DesignPattern.Interceptor
{
    public class GlobalInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
