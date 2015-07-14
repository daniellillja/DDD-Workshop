using System;
using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain
{
    public class ApplicationState : IAggregateState
    {
        public Applicant Applicant { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public Guid Id { get; set; }
        public CreditOffer Offer { get; set; }
    }
}