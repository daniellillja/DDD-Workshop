using System.Web.Http;
using DDD_Workshop.WebApi;
using Owin;

namespace DDD_Workshop.IntegrationTests.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // Execute any other ASP.NET Web API-related initialization, i.e. IoC, authentication, logging, mapping, DB, etc.
            //ConfigureAuthPipeline(app);
            app.UseWebApi(configuration);
        }
    }

}
