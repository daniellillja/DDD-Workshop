namespace DDD_Workshop.Infrastructure
{
    public interface IAggregateFactory<out TAggregate, in TAggregateState>
        where TAggregate : IAggregate<TAggregateState> where TAggregateState : IAggregateState
    {
        TAggregate CreateAggregate(IAggregateState state);
    }
}
