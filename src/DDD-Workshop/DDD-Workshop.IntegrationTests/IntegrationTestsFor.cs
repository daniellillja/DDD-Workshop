using NUnit.Framework;
using StructureMap;

namespace DDD_Workshop.IntegrationTests.Storage.Mongo
{
    [TestFixture]
    public class IntegrationTestsFor<TObjectUnderTest>
        where TObjectUnderTest : class
    {
        protected TObjectUnderTest ObjectUnderTest;

        [TestFixtureSetUp]
        public void Setup()
        {
            
            InitObjectUnderTest();
        }

        private void InitObjectUnderTest()
        {
            ObjectUnderTest = Container.GetInstance<TObjectUnderTest>();
        }

        protected IntegrationTestsFor()
        {
            ContainerBootstrapper.Bootstrap();
            Container = ObjectFactory.Container;
        }

        public IContainer Container { get; set; }
    }
}