using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using DDD_Workshop.Domain;
using DDD_Workshop.Domain.Application;
using DDD_Workshop.WebApi;
using DDD_Workshop.WebApi.Controllers.Application;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace DDD_Workshop.UnitTests.Api
{
    
    public class CustomControllerActivatorTests : UnitTestsFor<CustomControllerActivator>
    {
        [Test]
        public void ShouldResolveApplicationController()
        {
            For<IContainer>()
                .Setup(c => c.GetInstance(It.IsAny<Type>()))
                .Returns(new ApplicationController(new Mock<IApplicationService>().Object));

            var result = ObjectUnderTest.Create(
                new HttpRequestMessage(),
                new HttpControllerDescriptor(),
                typeof (ApplicationController));

            Assert.That(result, Is.TypeOf<ApplicationController>());
        }
    }
}
