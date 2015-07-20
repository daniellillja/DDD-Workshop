using System;
using DDD_Workshop.Domain;
using DDD_Workshop.Domain.Application;
using MongoRepository;

namespace DDD_Workshop.Data
{
    public class ApplicationStateDataModel : ApplicationState, IEntity<Guid>
    {
        public ApplicationStateDataModel(ApplicationState domain)
        {
            Applicant = domain.Applicant;
            ApplicationStatus = domain.ApplicationStatus;
            Id = domain.Id;
            Offer = domain.Offer;
        }
    }
}
