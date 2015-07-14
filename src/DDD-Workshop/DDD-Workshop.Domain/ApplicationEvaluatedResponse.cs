using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain
{
    public class ApplicationEvaluatedResponse : DomainResponse
    {
        public double APR { get; set; }
        public int CreditLimit { get; set; }
        public string ApplicationStatus { get; set; }

        public ApplicationEvaluatedResponse FromState(ApplicationState state)
        {
            ApplicationStatus = state.ApplicationStatus.Status;
            APR = state.Offer.APR;
            CreditLimit = state.Offer.CreditLimit;
            return this;
        }
    }
}