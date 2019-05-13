using Autofac.Integration.Mvc;
using ExpenseTracker.Starter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ExpenseTracker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Autofac configs

            var container = IoCInitializer.BuildAllMVCDependecies(typeof(MvcApplication).Assembly);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
