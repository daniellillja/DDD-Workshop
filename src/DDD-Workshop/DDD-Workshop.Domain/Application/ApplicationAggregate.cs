using System;
using System.Collections.Generic;
using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain.Application
{
    public class ApplicationAggregate : IAggregate<ApplicationState>
    {
        public ApplicationState State { get; set; }

        public ApplicationAggregate(ApplicationState state)
            : this()
        {
            State = state;
        }

        public ApplicationAggregate()
        {
            State = new ApplicationState();
        }

        public void AcceptApplication(Guid id, string firstName, string lastName)
        {
            State.Id = id;
            State.Applicant = new Applicant(firstName, lastName);
            State.ApplicationStatus = new ApplicationStatus()
                .Accepted();
        }

        public void EvaluateApplication(List<ApplicationState> allApplications)
        {
            if (ApplicantHasOtherApplications(allApplications))
            {
                State.ApplicationStatus = State.ApplicationStatus.ManualEvaluation();
            }

            State.Offer = new CreditOffer().DefaultOffer();
            State.ApplicationStatus = State.ApplicationStatus.Offered();
        }

        // TODO: may be best to extract to another interface
        private bool ApplicantHasOtherApplications(List<ApplicationState> allApplications)
        {
            return allApplications.Count > 1;
        }
    }

}