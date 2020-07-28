using System;
using System.Diagnostics;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace DesignPattern.Interceptor
{
    public class GlobalInterceptor : IInterceptor
    {
        private readonly ILogger<GlobalInterceptor> _logger;

        public GlobalInterceptor(ILogger<GlobalInterceptor> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var sw = new Stopwatch();
            sw.Start();
            invocation.Proceed();
            sw.Stop();
            _logger.LogInformation($"{invocation.Method.DeclaringType?.Name}.{invocation.Method.Name} 方法执行耗时：{sw.ElapsedMilliseconds}ms");
        }
    }
}
