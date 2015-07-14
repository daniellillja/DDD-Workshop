namespace DDD_Workshop.Infrastructure
{
    /// <summary>
    /// The aggregate contains domain logic performed on the
    /// aggregate state.
    /// </summary>
    /// <typeparam name="TAggregateState"></typeparam>
    public interface IAggregate<TAggregateState>
        where TAggregateState : IAggregateState
    {
        TAggregateState State { get; set; }
    }
}