using System.Runtime.InteropServices;
using MongoRepository;
using NUnit.Framework;
using StructureMap;

namespace DDD_Workshop.IntegrationTests
{
    public static class ContainerBootstrapper
    {
        [TestFixtureSetUp]
        public static void Bootstrap()
        {
            ObjectFactory.Initialize(x => x.AddRegistry<IntegrationTestsRegistry>());
        }
    }
}