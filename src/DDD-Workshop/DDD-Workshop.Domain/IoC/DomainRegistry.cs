using StructureMap.AutoMocking;
using StructureMap.Configuration.DSL;

namespace DDD_Workshop.Domain.IoC
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<IApplicationService>();
                s.WithDefaultConventions();
            });


        }

    }
}
