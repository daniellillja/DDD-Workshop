using DDD_Workshop.Data;
using DDD_Workshop.Data.IoC;
using DDD_Workshop.Domain.IoC;
using StructureMap.Configuration.DSL;

namespace DDD_Workshop.WebApi.IoC
{
    public class WebApiRegistry : Registry
    {
        public WebApiRegistry()
        {
            IncludeRegistry<DomainRegistry>();
            IncludeRegistry<DataRegistry>();
            For<IMongoDbSettings>().Use<MongoDbSettings>();
        }


    }
}