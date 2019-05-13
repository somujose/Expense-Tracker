using System.Linq;
using System.Reflection;
using Autofac;

namespace ExpenseTracker.Starter.Module
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("ExpenseTracker.Service"))
                 .Where(t => t.Name.EndsWith("Service"))
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope();
        }
    }
}
