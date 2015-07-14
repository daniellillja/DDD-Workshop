
using DDD_Workshop.Domain;
using DDD_Workshop.WebApi;
using DDD_Workshop.WebApi.IoC;
using NUnit.Framework;

namespace DDD_Workshop.UnitTests
{
    [TestFixture]
    public class WebApiContainerTests : UnitTestsFor<WebApiContainer>
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            InitObjectUnderTest();
        }

        [Test]
        public void ShouldResolveApplicationService()
        {
            var instance = ObjectUnderTest.GetInstance<IApplicationService>();
            Assert.That(instance, Is.TypeOf<ApplicationService>());
        }

    }
}
