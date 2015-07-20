using DDD_Workshop.Domain.Application;
using StructureMap.Configuration.DSL;

namespace DDD_Workshop.Data.IoC
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<ApplicatonRepository>();
                s.WithDefaultConventions();
            });
            //For<IApplicationRepository>().Use<ApplicationRepos>()
        }
    }
}
