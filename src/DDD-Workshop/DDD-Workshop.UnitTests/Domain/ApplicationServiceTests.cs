using System;
using System.Collections.Generic;
using DDD_Workshop.Domain;
using DDD_Workshop.Domain.Application;
using DDD_Workshop.Infrastructure;
using Moq;
using NUnit.Framework;

namespace DDD_Workshop.UnitTests.Domain
{
    public class ApplicationServiceTests : UnitTestsFor<ApplicationService>
    {
        private ApplicationSubmittedCommand _command;
        private ApplicationSubmittedResponse _result;
        private EvaluateApplicationCommand _evaluateApplicationCommand;
        private ApplicationEvaluatedResponse _evaluateResult;
        private List<ApplicationState> _outstandingApplications;
        private Applicant _applicant;
        private AcceptOfferCommand _acceptOfferCommand;
        private AcceptOfferResponse _acceptOfferResponse;

        [Test]
        public void HandleAcceptOfferCommandWithValidId()
        {
            GivenAAcceptOfferCommandWithValidId();
            WhenTheAcceptOfferCommandIsExecuted();
            ThenTheServiceShouldGetTheStateById();
            ThenTheServiceShouldCreateTheAggregate();
        }

        private void ThenTheServiceShouldCreateTheAggregate()
        {
            For<IAggregateFactory<ApplicationAggregate, ApplicationState>>()
                .Verify(f => f.CreateAggregate(Moq.It.IsAny<IAggregateState>()));
        }

        private void WhenTheAcceptOfferCommandIsExecuted()
        {
            _acceptOfferResponse = ObjectUnderTest.Handle(_acceptOfferCommand);
        }

        private void GivenAAcceptOfferCommandWithValidId()
        {
            _acceptOfferCommand = new AcceptOfferCommand(Guid.NewGuid());
            var agg = new ApplicationAggregate();
            For<IAggregateFactory<ApplicationAggregate, ApplicationState>>()
                .Setup(f => f.CreateAggregate(It.IsAny<ApplicationState>()))
                .Returns(agg);
        }

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

        [Test]
        public void HandleEvaluateApplicationCommandOutstandingApplications()
        {
            GivenAnEvaluationApplicationCommand();
            GivenTheApplicationIdExists(_evaluateApplicationCommand.Id);
            GivenThereAreOtherApplicationsOutstanding();
            WhenTheEvaluateApplicationCommandIsExecuted();
            ThenTheEvaluatedApplicationStateShouldBe("Manual Evaluation");
        }

        private void ThenTheEvaluatedApplicationStateShouldBe(string status)
        {
            Assert.That(_evaluateResult.ApplicationStatus, Is.EqualTo(status));
        }


        private void GivenThereAreOtherApplicationsOutstanding()
        {
            _applicant = new Applicant("Daniel", "Lillja");
            _outstandingApplications = new List<ApplicationState>()
            {
                // application that was previously submitted
                new ApplicationState() { Applicant = _applicant,
                    ApplicationStatus = new ApplicationStatus().Accepted(),
                    Id = _evaluateApplicationCommand.Id},
                new ApplicationState() { Applicant = _applicant,
                    ApplicationStatus = new ApplicationStatus().Accepted().Offered(),
                    Id = _evaluateApplicationCommand.Id, Offer = new CreditOffer().DefaultOffer()},

            };
            For<IApplicationRepository>()
                .Setup(r => r.GetApplicationsWithApplicant(It.IsAny<Applicant>()))
                .Returns(_outstandingApplications);
        }

        private void ThenTheApplicationShouldBeOffered(int creditLimit, double apr)
        {
            Assert.That(_evaluateResult.ApplicationStatus, Is.EqualTo("Offered"));
            Assert.That(_evaluateResult.APR, Is.EqualTo(apr));
            Assert.That(_evaluateResult.CreditLimit, Is.EqualTo(creditLimit));
        }

        private void GivenThereAreNoOtherApplicationsOutstanding()
        {
            _outstandingApplications = new List<ApplicationState>()
            {
                // application that was previously submitted
                new ApplicationState()
            };
            For<IApplicationRepository>()
                .Setup(r => r.GetApplicationsWithApplicant(It.IsAny<Applicant>()))
                .Returns(_outstandingApplications);
        }

        private void GivenTheApplicationIdExists(Guid id)
        {
            var state = new ApplicationState()
            {
                Id = id,
                Applicant = new Applicant("Daniel", "Lillja"),
                ApplicationStatus = new ApplicationStatus().Accepted()
            };
            var agg = new ApplicationAggregate();
            agg.State = state;
            For<IApplicationRepository>()
                .Setup(r => r.GetApplicationById(It.IsAny<Guid>()))
                .Returns(state);
            For<IAggregateFactory<ApplicationAggregate, ApplicationState>>()
                .Setup(f => f.CreateAggregate(It.IsAny<ApplicationState>()))
                .Returns(agg);

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
                .Verify(r => r.SaveApplication(It.IsAny<ApplicationState>()));
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
            var agg = new ApplicationAggregate();
            For<IAggregateFactory<ApplicationAggregate, ApplicationState>>()
                .Setup(f => f.CreateAggregate(It.IsAny<ApplicationState>()))
                .Returns(agg);
        }
    }
}
