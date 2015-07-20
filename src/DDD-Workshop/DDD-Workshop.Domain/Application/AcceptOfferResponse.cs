using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain.Application
{
    public class AcceptOfferResponse : DomainResponse
    {
        public string AccountNumber { get; set; }
        public string AccountActivationStatus { get; set; }
    }
}