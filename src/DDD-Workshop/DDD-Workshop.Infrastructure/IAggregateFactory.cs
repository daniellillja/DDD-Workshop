namespace DDD_Workshop.Infrastructure
{
    public interface IAggregateFactory<TAggregate, TAggregateState>
        where TAggregate : IAggregate<TAggregateState> where TAggregateState : IAggregateState
    {
        TAggregate CreateAggregate(IAggregateState state);
    }
}
