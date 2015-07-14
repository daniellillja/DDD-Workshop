using System;
using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain
{
    public class AcceptOfferCommand : DomainCommand
    {
        public AcceptOfferCommand(Guid applicationId)
        {
            ApplicationId = applicationId;
        }

        public Guid ApplicationId { get; set; }
    }
}