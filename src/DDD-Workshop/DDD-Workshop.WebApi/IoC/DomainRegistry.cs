using DDD_Workshop.Domain.Application;
using DDD_Workshop.Infrastructure;
using DDD_Workshop.WebApi.IoC;
using StructureMap.Configuration.DSL;

namespace DDD_Workshop.Domain.IoC
{
    public class DomainRegistry : Registry
    {
        //TODO: may want to move this out of domain assembly - Domain assembly should not have external libraries (structuremap)?
        public DomainRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<IApplicationService>();
                s.WithDefaultConventions();
            });

            For(typeof (IAggregateFactory<,>))
                .Use(typeof (AggregateFactory<,>));
        }

    }
}
