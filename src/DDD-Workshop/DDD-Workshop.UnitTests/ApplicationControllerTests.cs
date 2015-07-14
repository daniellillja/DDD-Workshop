using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.Results;
using DDD_Workshop.Domain;
using DDD_Workshop.WebApi.Controllers.Application;
using NUnit.Framework;

namespace DDD_Workshop.UnitTests
{
    [TestFixture]
    public class ApplicationControllerTests : UnitTestsFor<ApplicationController>
    {
        private IHttpActionResult _result;
        private SubmitApplicationRequest _request;

        [TestFixtureSetUp]
        public void Setup()
        {
            InitObjectUnderTest();
        }

        [Test]
        public void WhenASubmitApplicationRequestIsPosted()
        {
            GivenAValidApplicationSubmittedRequest();
            WhenTheRequestIsPosted();
            ThenTheApplicationServiceSubmitApplicationMethodShouldBeCalled();
            ThenTheApiShouldReturnOk();
        }

        private void ThenTheApiShouldReturnOk()
        {
            Assert.That(_result, 
                Is.TypeOf<OkNegotiatedContentResult<ApplicationSubmittedResponse>>());
        }


        private void GivenAValidApplicationSubmittedRequest()
        {
            _request = new SubmitApplicationRequest()
            {
                FirstName = "Daniel",
                LastName = "Lillja"
            };

            For<IApplicationService>()
                .Setup(s => s.Handle(Moq.It.IsAny<ApplicationSubmittedCommand>()))
                .Returns(new ApplicationSubmittedResponse());

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
