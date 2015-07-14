using DDD_Workshop.Domain.IoC;
using StructureMap.Configuration.DSL;

namespace DDD_Workshop.WebApi.IoC
{
    public class WebApiRegistry : Registry
    {
        public WebApiRegistry()
        {
            IncludeRegistry<DomainRegistry>();
        }
    }
}