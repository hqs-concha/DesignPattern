using System;
using System.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy;
using DesignPattern.Interceptor;
using DesignPattern.Interface;
using DesignPattern.Interface.Event;
using DesignPattern.Services;
using DesignPattern.Services.Event;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesignPattern
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();

            #region ChangeToken

            services.AddSingleton<ICustomMonitor, CustomMonitor>();

            #endregion

            #region Event

            services.AddScoped<IEventBus, EventBus>();

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(p => p.GetTypes().Where(x => x.IsClass && x.GetInterfaces().Contains(typeof(IEventHandler))))
                .ToArray();

            foreach (var item in types)
            {
                services.AddScoped(item);
            }

            #endregion
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<CustomInterceptor>();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope().EnableInterfaceInterceptors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
