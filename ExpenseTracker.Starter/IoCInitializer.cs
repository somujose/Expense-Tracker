using Autofac;
using Autofac.Integration.Mvc;
using ExpenseTracker.Starter.Module;

namespace ExpenseTracker.Starter
{
    public class IoCInitializer
    {
        public static IContainer BuildAllMVCDependecies(System.Reflection.Assembly mvcAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(mvcAssembly).PropertiesAutowired();

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());

            return builder.Build();
        }
    }
}
