using System;
using System.Collections.Generic;
using DDD_Workshop.Domain;
using Moq;
using NUnit.Framework;

namespace DDD_Workshop.UnitTests.Api
{
    public class ApplicationServiceTests : UnitTestsFor<ApplicationService>
    {
        private ApplicationSubmittedCommand _command;
        private ApplicationSubmittedResponse _result;
        private EvaluateApplicationCommand _evaluateApplicationCommand;
        private ApplicationEvaluatedResponse _evaluateResult;
        private List<Application> _outstandingApplications;

        [Test]
        public void HandleApplicationSubmitCommandWithFirstAndLastName()
        {
            GivenACommandWithFirstAndLastName("Daniel", "Lillja");
            WhenTheApplicationSubmittedCommandIsExecuted();
            ThenTheApplicationStateShouldBeSaved();
            ThenTheApplicationStateShouldBeAccepted();
        }

        [Test]
        public void HandleEvaluateApplicationCommand()
        {
            GivenAnEvaluationApplicationCommand();
            GivenTheApplicationIdExists(_evaluateApplicationCommand.Id);
            GivenThereAreNoOtherApplicationsOutstanding();
            WhenTheEvaluateApplicationCommandIsExecuted();
            ThenTheServiceShouldGetTheStateById();
            ThenTheServiceShouldCheckWhetherThereAreOtherApplications();
            ThenTheApplicationShouldBeOffered(5000, 29.99);
        }

        private void ThenTheApplicationShouldBeOffered(int creditLimit, double apr)
        {
            Assert.That(_evaluateResult.ApplicationStatus, Is.EqualTo("Offered"));
            Assert.That(_evaluateResult.APR, Is.EqualTo(apr));
            Assert.That(_evaluateResult.CreditLimit, Is.EqualTo(creditLimit));
        }

        private void GivenThereAreNoOtherApplicationsOutstanding()
        {
            _outstandingApplications = new List<Application>()
            {
                // application that was previously submitted
                new Application()
            };
            For<IApplicationRepository>()
                .Setup(r => r.GetApplicationsWithApplicant(It.IsAny<Applicant>()))
                .Returns(_outstandingApplications);
        }

        private void GivenTheApplicationIdExists(Guid id)
        {
            For<IApplicationRepository>()
                .Setup(r => r.GetApplicationById(It.IsAny<Guid>()))
                .Returns(new Application()
                {
                    Id = id, Applicant = new Applicant("Daniel", "Lillja"), ApplicationStatus = new ApplicationStatus().Accepted()
                });

        }

        private void ThenTheServiceShouldGetTheStateById()
        {
            For<IApplicationRepository>()
                .Verify(r => r.GetApplicationById(It.IsAny<Guid>()));
        }

        private void ThenTheServiceShouldCheckWhetherThereAreOtherApplications()
        {
            For<IApplicationRepository>()
                .Verify(m => m.GetApplicationsWithApplicant(Moq.It.IsAny<Applicant>()));
        }

        private void WhenTheEvaluateApplicationCommandIsExecuted()
        {
            _evaluateResult = ObjectUnderTest.Handle(_evaluateApplicationCommand);
        }

        private void GivenAnEvaluationApplicationCommand()
        {
            _evaluateApplicationCommand = new EvaluateApplicationCommand(Guid.NewGuid());
        }

        private void ThenTheApplicationStateShouldBeSaved()
        {
            For<IApplicationRepository>()
                .Verify(r => r.SaveApplication(It.IsAny<Application>()));
        }

        private void ThenTheApplicationStateShouldBeAccepted()
        {
            Assert.That(_result.ApplicationStatus, Is.EqualTo("Accepted"));
        }

        private void WhenTheApplicationSubmittedCommandIsExecuted()
        {
            _result = ObjectUnderTest.Handle(_command);
        }

        private void GivenACommandWithFirstAndLastName(string firstName, string lastName)
        {
            _command = new ApplicationSubmittedCommand(firstName, lastName);
        }
    }
}
