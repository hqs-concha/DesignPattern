using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using DesignPattern.Interceptor;
using DesignPattern.Interface;
using Module = Autofac.Module;

namespace DesignPattern.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomInterceptor>();
            builder.RegisterType<GlobalInterceptor>();

            RegisterScopeService(builder);
            RegisterSingletonService(builder);
            RegisterTransientService(builder);
        }

        private void RegisterTransientService(ContainerBuilder builder)
        {
            var baseType = typeof(ITransientService);
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => baseType.IsAssignableFrom(t) && t.IsClass)
                .AsImplementedInterfaces().InstancePerDependency()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(GlobalInterceptor), typeof(CustomInterceptor));
        }

        private void RegisterScopeService(ContainerBuilder builder)
        {
            var baseType = typeof(IScopeService);
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => baseType.IsAssignableFrom(t) && t.IsClass)
                .AsImplementedInterfaces().InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(GlobalInterceptor), typeof(CustomInterceptor));
        }

        private void RegisterSingletonService(ContainerBuilder builder)
        {
            var baseType = typeof(ISingleton);
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => baseType.IsAssignableFrom(t) && t.IsClass)
                .AsImplementedInterfaces().SingleInstance()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(GlobalInterceptor), typeof(CustomInterceptor));
        }
    }
}
