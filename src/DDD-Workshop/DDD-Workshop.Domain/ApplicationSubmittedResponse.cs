using System;
using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain
{
    public class ApplicationSubmittedResponse : DomainResponse
    {
        public string ApplicationStatus { get; set; }
        public Guid ApplicationId { get; set; }

        public ApplicationSubmittedResponse FromState(ApplicationState state)
        {
            ApplicationStatus = state.ApplicationStatus.Status;
            ApplicationId = state.Id;
            return this;
        }
    }
}