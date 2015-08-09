using System;
using System.Web.Http;
using System.Web.Http.Results;
using DDD_Workshop.Domain;
using DDD_Workshop.Domain.Application;
using DDD_Workshop.WebApi.Controllers.Application;
using Moq;
using NUnit.Framework;

namespace DDD_Workshop.UnitTests
{
    [TestFixture]
    public class ApplicationControllerTests : UnitTestsFor<ApplicationsController>
    {
        private IHttpActionResult _result;
        private SubmitApplicationRequest _request;
        private ApplicationSubmittedResponse _submitReponse;
        private ApplicationEvaluatedResponse _evaluateResponse;
        private AcceptOfferRequest _acceptOfferRequest;
        private AcceptOfferResponse _acceptOfferResponse;

        [Test]
        public void WhenASubmitApplicationRequestIsPosted()
        {
            GivenAValidApplicationSubmittedRequest();
            WhenTheRequestIsPosted();
            ThenTheApplicationServiceSubmitApplicationMethodShouldBeCalled();
            ThenTheApplicationServiceEvaluateMethodShouldBeACalled();
            ThenTheApiShouldReturnOk();
        }

        [Test]
        public void WhenAnAcceptOfferRequestIsPosted()
        {
            GivenAValidAcceptOfferRequestIsPosted();
            WhenTheRequestIsProcessed();
            ThenTheApplicationServiceAcceptOfferShouldBeCalled();
            ThenTheApiShouldReturnOk<AcceptOfferResponse>();
        }

        private void ThenTheApiShouldReturnOk<T>()
        {
            Assert.That(_result.GetType(), Is.EqualTo(typeof(OkNegotiatedContentResult<T>)));
        }

        private void ThenTheApplicationServiceAcceptOfferShouldBeCalled()
        {
            For<IApplicationService>()
                .Verify(s => s.Handle(It.IsAny<AcceptOfferCommand>()));
        }

        private void WhenTheRequestIsProcessed()
        {
            _result = ObjectUnderTest.PostAcceptOfferRequest(_acceptOfferRequest);
        }

        private void GivenAValidAcceptOfferRequestIsPosted()
        {
            _acceptOfferRequest = new AcceptOfferRequest() {ApplicationId = Guid.NewGuid()};
            _acceptOfferResponse = new AcceptOfferResponse() {AccountNumber = "213423", AccountActivationStatus = "Unactivated"};
            For<IApplicationService>()
                .Setup(s => s.Handle(It.IsAny<AcceptOfferCommand>()))
                .Returns(_acceptOfferResponse);
        }

        private void ThenTheApplicationServiceEvaluateMethodShouldBeACalled()
        {
            For<IApplicationService>()
                .Verify(s => s.Handle(It.IsAny<EvaluateApplicationCommand>()));
        }

        private void ThenTheApiShouldReturnOk()
        {
            Assert.That(_result, 
                Is.TypeOf<OkNegotiatedContentResult<ApplicationEvaluatedResponse>>());
        }


        private void GivenAValidApplicationSubmittedRequest()
        {
            _request = new SubmitApplicationRequest()
            {
                FirstName = "Daniel",
                LastName = "Lillja"
            };
            _submitReponse = new ApplicationSubmittedResponse()
            {
                ApplicationStatus = new ApplicationStatus().Accepted().Status,
                ApplicationId = Guid.NewGuid()
            };

            var offerData = new CreditOffer().DefaultOffer();
            _evaluateResponse = new ApplicationEvaluatedResponse()
            {
                ApplicationStatus = new ApplicationStatus().Accepted().Offered().Status,
                APR = offerData.APR,
                CreditLimit = offerData.CreditLimit
            };

            For<IApplicationService>()
                .Setup(s => s.Handle(It.IsAny<ApplicationSubmittedCommand>()))
                .Returns(_submitReponse);
            For<IApplicationService>()
                .Setup(s => s.Handle(It.IsAny<EvaluateApplicationCommand>()))
                .Returns(_evaluateResponse);

        }

        private void ThenTheApplicationServiceSubmitApplicationMethodShouldBeCalled()
        {
            For<IApplicationService>()
                .Verify(s => s.Handle(It.IsAny<ApplicationSubmittedCommand>()));
        }

        private void WhenTheRequestIsPosted()
        {
            _result = ObjectUnderTest.PostSubmitApplicationRequest(_request);
        }
    }

}
