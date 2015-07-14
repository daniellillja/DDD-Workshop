using System;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.Results;
using DDD_Workshop.Domain;
using DDD_Workshop.WebApi.Controllers.Application;
using Moq;
using NUnit.Framework;

namespace DDD_Workshop.UnitTests
{
    [TestFixture]
    public class ApplicationControllerTests : UnitTestsFor<ApplicationController>
    {
        private IHttpActionResult _result;
        private SubmitApplicationRequest _request;
        private ApplicationSubmittedResponse _submitReponse;
        private ApplicationEvaluatedResponse _evaluateResponse;

        [Test]
        public void WhenASubmitApplicationRequestIsPosted()
        {
            GivenAValidApplicationSubmittedRequest();
            WhenTheRequestIsPosted();
            ThenTheApplicationServiceSubmitApplicationMethodShouldBeCalled();
            ThenTheApplicationServiceEvaluateMethodShouldBeACalled();
            ThenTheApiShouldReturnOk();
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
                .Setup(s => s.Handle(Moq.It.IsAny<ApplicationSubmittedCommand>()))
                .Returns(_submitReponse);
            For<IApplicationService>()
                .Setup(s => s.Handle(Moq.It.IsAny<EvaluateApplicationCommand>()))
                .Returns(_evaluateResponse);

        }

        private void ThenTheApplicationServiceSubmitApplicationMethodShouldBeCalled()
        {
            For<IApplicationService>()
                .Verify(s => s.Handle(Moq.It.IsAny<ApplicationSubmittedCommand>()));
        }

        private void WhenTheRequestIsPosted()
        {
            _result = ObjectUnderTest.PostSubmitApplicationRequest(_request);
        }
    }
}
