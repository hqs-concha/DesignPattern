using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using DesignPattern.Attributes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace DesignPattern.Interceptor
{
    public class CustomInterceptor : IInterceptor
    {
        private readonly ILogger<CustomInterceptor> _logger;
        private readonly IMemoryCache _cache;

        public CustomInterceptor(ILogger<CustomInterceptor> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                var cacheAttr = invocation.Method.GetCustomAttributes<CacheAttribute>(true).FirstOrDefault();
                if (cacheAttr != null)
                {
                    _logger.LogInformation($"拦截方法：{invocation.Method.DeclaringType?.Name}.{invocation.Method.Name}");

                    var argument = invocation.Arguments.FirstOrDefault();
                    var value = _cache.Get($"{cacheAttr.Name}-{argument}");
                    if (value != null)
                        invocation.ReturnValue = value;
                    else
                    {
                        invocation.Proceed();
                        value = invocation.ReturnValue;
                        _cache.Set($"{cacheAttr.Name}-{argument}", value, TimeSpan.FromHours(1));
                    }
                
                    _logger.LogInformation($"{DateTime.Now:s}，方法返回值：{JsonSerializer.Serialize(value)}");
                }
                else
                {
                    invocation.Proceed();
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"出错了：{e.Message}");
                invocation.ReturnValue = new {success = 1, message = e.Message};
            }
        }

        private object GetReturnValue(IInvocation invocation)
        {
            var value = invocation.ReturnValue;
            if (invocation.Method.ReturnType.BaseType == typeof(Task))
            {
                var task = (Task<object>) value;
                value = task.Result;
            }

            return value;
        }
    }
}
