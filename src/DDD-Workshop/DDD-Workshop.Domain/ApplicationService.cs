using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain
{
    public interface IApplicationService
    {
        ApplicationSubmittedResponse Handle(ApplicationSubmittedCommand command);
    }

    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }


        public ApplicationSubmittedResponse Handle(ApplicationSubmittedCommand command)
        {
            var id = Guid.NewGuid();
            var aggregate = new ApplicationAggregate(null);
            aggregate.AcceptApplication(id, command.FirstName,
                command.LastName);

            var state = (Application)aggregate.GetState();
            _applicationRepository.SaveApplication(state);

            var response = new ApplicationSubmittedResponse()
            {
                ApplicationStatus = state.ApplicationStatus.Status,
                ApplicationId = state.Id
            };

            return response;
        }

        public ApplicationEvaluatedResponse Handle(EvaluateApplicationCommand command)
        {
            var state = _applicationRepository.GetApplicationById(command.Id);
            var allApplications = _applicationRepository.GetApplicationsWithApplicant(
                new Applicant(state.Applicant.FirstName, state.Applicant.LastName));
            var aggregate = new ApplicationAggregate(state);

            aggregate.EvaluateApplication(allApplications);

            var response = new ApplicationEvaluatedResponse()
            {
                ApplicationStatus = state.ApplicationStatus.Status,
                APR = state.Offer.APR,
                CreditLimit = state.Offer.CreditLimit
            };

            return response;
        }
    }

    public class ApplicationEvaluatedResponse
    {
        public double APR { get; set; }
        public int CreditLimit { get; set; }
        public string ApplicationStatus { get; set; }
    }

    public class EvaluateApplicationCommand
    {
        public Guid Id { get; set; }


        public EvaluateApplicationCommand(Guid id)
        {
            Id = id;
        }
    }

    public interface IApplicationRepository
    {
        List<Application> GetApplicationsWithApplicant(Applicant applicant);
        void SaveApplication(Application application);
        Application GetApplicationById(Guid id);
    }

    public class Application
    {
        public Applicant Applicant { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public Guid Id { get; set; }
        public CreditOffer Offer { get; set; }
    }

    public class ApplicationStatus
    {
        public string Status { get; private set; }

        public ApplicationStatus Accepted()
        {
            Status = "Accepted";
            return this;
        }

        public ApplicationStatus Offered()
        {
            if (Status.Equals("Accepted"))
            {
                Status = "Offered";
            }

            return this;
        }
    }

    public class ApplicationAggregate : IAggregate
    {
        private Application _state;

        public ApplicationAggregate(Application state)
        {
            _state = state ?? new Application();
        }

        public void AcceptApplication(Guid id, string firstName, string lastName)
        {
            _state.Id = id;
            _state.Applicant = new Applicant(firstName, lastName);
            _state.ApplicationStatus = new ApplicationStatus()
                .Accepted();
        }

        public object GetState()
        {
            return _state;
        }

        public void EvaluateApplication(List<Application> allApplications)
        {
            if (ApplicantHasOtherApplications(allApplications))
            {
                throw new NotImplementedException();
            }

            _state.Offer = new CreditOffer().DefaultOffer();
            _state.ApplicationStatus = _state.ApplicationStatus.Offered();
        }

        private bool ApplicantHasOtherApplications(List<Application> allApplications)
        {
            return allApplications.Count > 1;
        }
    }

    public class CreditOffer
    {
        public CreditOffer DefaultOffer()
        {
            APR = 29.99;
            CreditLimit = 5000;

            return this;
        }

        public int CreditLimit { get; private set; }

        public double APR { get; private set; }
    }


    public class ApplicationSubmittedResponse
    {
        public string ApplicationStatus { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
