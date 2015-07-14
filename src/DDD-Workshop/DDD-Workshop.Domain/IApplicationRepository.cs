using System;
using System.Collections.Generic;

namespace DDD_Workshop.Domain
{
    public interface IApplicationRepository
    {
        List<ApplicationState> GetApplicationsWithApplicant(Applicant applicant);
        void SaveApplication(ApplicationState application);
        ApplicationState GetApplicationById(Guid id);
    }
}