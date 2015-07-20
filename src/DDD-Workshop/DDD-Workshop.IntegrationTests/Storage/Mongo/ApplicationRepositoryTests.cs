using System;
using System.Diagnostics;
using System.Linq;
using DDD_Workshop.Data;
using DDD_Workshop.Domain;
using DDD_Workshop.Domain.Application;
using NUnit.Framework;

namespace DDD_Workshop.IntegrationTests.Storage.Mongo
{
    public class ApplicationRepositoryTests : RepositoryTestsFor<ApplicatonRepository>
    {
        private ApplicationState _applicationToSave;
        private ApplicationState _applicationRetreived;

        [Test]
        public void SaveApplicationSaves()
        {
            GivenAValidApplicationToSave();
            WhenISaveTheApplication(_applicationToSave);
            ThenTheApplicationCanBeRetreivedById(_applicationToSave);
            ThenTheApplicationFieldsAreSaved(_applicationToSave, _applicationRetreived);
            ThenTheApplicationCanRetreivedByApplicant(_applicationToSave.Applicant);
        }

        private void ThenTheApplicationCanRetreivedByApplicant(Applicant applicant)
        {
            var application = ObjectUnderTest.GetApplicationsWithApplicant(applicant)
                .FirstOrDefault();
            Assert.That(application, Is.Not.Null);
            Assert.That(application.Applicant.FirstName, Is.EqualTo(applicant.FirstName));
        }

        private void ThenTheApplicationFieldsAreSaved(ApplicationState applicationToSave, ApplicationState applicationRetreived)
        {
            Assert.That(applicationToSave.Id, Is.EqualTo(_applicationRetreived.Id));
            Assert.That(applicationToSave.Applicant.FirstName, Is.EqualTo(_applicationRetreived.Applicant.FirstName));
            Assert.That(applicationToSave.Offer.APR, Is.EqualTo(_applicationRetreived.Offer.APR));
        }

        private void ThenTheApplicationCanBeRetreivedById(ApplicationState application)
        {
            _applicationRetreived = ObjectUnderTest.GetApplicationById(application.Id);
        }

        private void WhenISaveTheApplication(ApplicationState applicationToSave)
        {
            ObjectUnderTest.SaveApplication(applicationToSave);
        }

        private void GivenAValidApplicationToSave()
        {
            _applicationToSave = new ApplicationState()
            {
                Applicant = new Applicant("Daniel", "Lillja"),
                ApplicationStatus = new ApplicationStatus().Accepted(),
                Id = Guid.NewGuid(),
                Offer = new CreditOffer().DefaultOffer()
            };
        }

        public override void ClearData()
        {
            var manager = Container.GetInstance<DomainStateRepository<ApplicationStateDataModel>>();
            manager.Collection.Drop();
        }
    }

}
