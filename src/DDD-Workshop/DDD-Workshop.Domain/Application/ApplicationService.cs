using System;
using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain.Application
{
    public class ApplicationService
        : DomainService<ApplicationAggregate, ApplicationState>,
            IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IAggregateFactory<ApplicationAggregate, ApplicationState> aggregateFactory,
            IApplicationRepository applicationRepository)
            : base(aggregateFactory)
        {
            _applicationRepository = applicationRepository;
        }

        public AcceptOfferResponse Handle(AcceptOfferCommand command)
        {
            var state = _applicationRepository.GetApplicationById(command.ApplicationId);
            var aggregate = _AggregateFactory.CreateAggregate(state);

            return null;
        }

        public ApplicationEvaluatedResponse Handle(EvaluateApplicationCommand command)
        {
            var state = _applicationRepository.GetApplicationById(command.Id);
            var allApplications = _applicationRepository.GetApplicationsWithApplicant(
                new Applicant(state.Applicant.FirstName, state.Applicant.LastName));
            var aggregate = _AggregateFactory.CreateAggregate(state);

            aggregate.EvaluateApplication(allApplications);

            var response = new ApplicationEvaluatedResponse().FromState(state);

            return response;
        }

        public ApplicationSubmittedResponse Handle(ApplicationSubmittedCommand command)
        {
            var id = Guid.NewGuid();
            var aggregate = _AggregateFactory.CreateAggregate(null);
            aggregate.AcceptApplication(id, command.FirstName,
                command.LastName);

            var state = aggregate.State;
            _applicationRepository.SaveApplication(state);
            var response = new ApplicationSubmittedResponse().FromState(state);

            return response;
        }
    }

   
}
