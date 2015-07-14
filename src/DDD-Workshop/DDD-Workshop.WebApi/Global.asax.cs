using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DDD_Workshop.Domain;
using DDD_Workshop.WebApi.IoC;
using StructureMap.Graph;

namespace DDD_Workshop.WebApi
{
    
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SetupDependencyInjection();
        }

        private void SetupDependencyInjection()
        {
            var container = new WebApiContainer();
            var customControllerActivator =
                new CustomControllerActivator(container);
        }
    }
}
