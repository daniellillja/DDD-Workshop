namespace DDD_Workshop.Infrastructure
{
    public interface IAggregate
    {
        object GetState();
    }
}
