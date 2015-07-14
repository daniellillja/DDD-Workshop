namespace DDD_Workshop.Infrastructure
{
    public interface IHandleDomainCommands<in TCommand, out TResponse> 
        where TCommand : DomainCommand
        where TResponse : DomainResponse
    {
        TResponse Handle(TCommand command);
    }
}