using DDD_Workshop.Infrastructure;
using StructureMap;

namespace DDD_Workshop.WebApi.IoC
{
    public class AggregateFactory<TAggregate, TAggregateState>
        : IAggregateFactory<TAggregate, TAggregateState> 
        where TAggregateState : IAggregateState
        where TAggregate : IAggregate<TAggregateState>
    {
        private readonly IContainer _container;

        public AggregateFactory(IContainer container)
        {
            _container = container;
        }
        public TAggregate CreateAggregate(IAggregateState state)
        {
            var agg = _container.GetInstance<TAggregate>();
            agg.State = (TAggregateState)state;
            return agg;
        }
    }
}