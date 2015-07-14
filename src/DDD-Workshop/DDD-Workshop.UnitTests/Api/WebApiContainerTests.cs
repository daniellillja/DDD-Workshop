
using DDD_Workshop.Domain;
using DDD_Workshop.WebApi.IoC;
using NUnit.Framework;

namespace DDD_Workshop.UnitTests
{
    public class WebApiContainerTests : UnitTestsFor<WebApiContainer>
    {
        [Test]
        public void ShouldResolveApplicationService()
        {
            var instance = ObjectUnderTest.GetInstance<IApplicationService>();
            Assert.That(instance, Is.TypeOf<ApplicationService>());
        }

    }
}
