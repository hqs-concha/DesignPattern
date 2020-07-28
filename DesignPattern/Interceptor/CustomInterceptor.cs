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
                    var value = _cache.Get<string>($"{cacheAttr.Key}-{argument}");
                    if (value != null)
                    {
                        ReturnMethod(value, invocation);
                        return;
                    }
                    else
                    {
                        invocation.Proceed();
                        value = GetReturnValue(invocation);
                        _cache.Set($"{cacheAttr.Key}-{argument}", value, TimeSpan.FromMinutes(cacheAttr.ExpireMinutes));
                    }

                    _logger.LogInformation($"{DateTime.Now:s}，方法返回值：{value}");
                }
                else
                {
                    invocation.Proceed();
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"出错了：{e.Message}");
                invocation.ReturnValue = new { success = 1, message = e.Message };
            }
        }

        private void ReturnMethod(string returnValue, IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;

            var returnType = typeof(Task).IsAssignableFrom(method.ReturnType)
                ? method.ReturnType.GenericTypeArguments.FirstOrDefault()
                : method.ReturnType;

            dynamic result = JsonSerializer.Deserialize(returnValue, returnType);
            invocation.ReturnValue = typeof(Task).IsAssignableFrom(method.ReturnType) ? Task.FromResult(result) : result;
        }


        private string GetReturnValue(IInvocation invocation)
        {
            var value = invocation.ReturnValue;
            var type = invocation.Method.ReturnType;
            if (typeof(Task).IsAssignableFrom(type))
            {
                var resultProperty = type.GetProperty("Result");
                value = resultProperty.GetValue(invocation.ReturnValue);
            }

            return JsonSerializer.Serialize(value);
        }
    }
}
