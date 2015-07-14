using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain
{
    public class AcceptOfferResponse : DomainResponse
    {
        public string AccountNumber { get; set; }
        public string AccountActivationStatus { get; set; }
    }
}