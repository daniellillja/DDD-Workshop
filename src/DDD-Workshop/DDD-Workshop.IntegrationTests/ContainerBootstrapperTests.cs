using NUnit.Framework;
using StructureMap;

namespace DDD_Workshop.IntegrationTests
{
    [TestFixture]
    public class ContainerBootstrapperTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            ContainerBootstrapper.Bootstrap();
        }
        [Test]
        public void ConfigurationIsValid()
        {
            ObjectFactory.Container.AssertConfigurationIsValid();
            
        }
    }
}