using NUnit.Framework;

namespace DDD_Workshop.IntegrationTests.Storage.Mongo
{
    public abstract class RepositoryTestsFor<TObjectUnderTest>
        : IntegrationTestsFor<TObjectUnderTest> where TObjectUnderTest : class
    {
        public RepositoryTestsFor()
        {

        }

        [SetUp]
        public abstract void ClearData();
    }
}