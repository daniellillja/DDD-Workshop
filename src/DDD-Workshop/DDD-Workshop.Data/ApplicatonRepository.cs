using System;
using System.Collections.Generic;
using System.Linq;
using DDD_Workshop.Domain.Application;

namespace DDD_Workshop.Data
{
    public class ApplicatonRepository : IApplicationRepository
    {
        private readonly DomainStateRepository<ApplicationStateDataModel> _mongoRepository;

        public ApplicatonRepository(DomainStateRepository<ApplicationStateDataModel> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public List<ApplicationState> GetApplicationsWithApplicant(Applicant applicant)
        {
            return _mongoRepository.Where(a => a.Applicant.Equals(applicant))
                .Select(a => (ApplicationState) a).ToList();
        }

        public void SaveApplication(ApplicationState application)
        {
            var dataModel = new ApplicationStateDataModel(application);
            _mongoRepository.Update(dataModel);
        }

        public ApplicationState GetApplicationById(Guid id)
        {
           return _mongoRepository.GetById(id);
        }
    }
}
