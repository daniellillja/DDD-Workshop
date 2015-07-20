using DDD_Workshop.Data;
using StructureMap.Configuration.DSL;

namespace DDD_Workshop.IntegrationTests
{
    public class IntegrationTestsRegistry : Registry
    {
        public IntegrationTestsRegistry()
        {
            For<IMongoDbSettings>().Use<IntegrationMongoDbSettings>();

            For(typeof (DomainStateRepository<>))
                .Use((typeof (DomainStateRepository<>)));
            For(typeof (DomainStateRepository<>))
                .Use(typeof (DomainStateRepository<>));
        }
    }
}