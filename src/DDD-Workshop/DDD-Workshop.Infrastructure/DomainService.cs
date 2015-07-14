namespace DDD_Workshop.Infrastructure
{
    public abstract class DomainService<TAggregate, TAggregateState>
        where TAggregate : IAggregate<TAggregateState> where TAggregateState : IAggregateState
    {
        protected readonly IAggregateFactory<TAggregate, TAggregateState> _AggregateFactory;

        protected DomainService(IAggregateFactory<TAggregate, TAggregateState> aggregateFactory)
        {
            _AggregateFactory = aggregateFactory;
        }
    }
}
