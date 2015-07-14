using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain
{
    public interface IApplicationService :
        IHandleDomainCommands<AcceptOfferCommand, AcceptOfferResponse>,
        IHandleDomainCommands<EvaluateApplicationCommand, ApplicationEvaluatedResponse>,
        IHandleDomainCommands<ApplicationSubmittedCommand, ApplicationSubmittedResponse>
    {

    }
}