using System;
using System.Diagnostics;
using System.Linq.Expressions;
using DDD_Workshop.Data;
using DDD_Workshop.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace DDD_Workshop.IntegrationTests.Storage.Mongo
{
    [TestFixture]
    public class IntegrationMongoDbSettingsTest
    {
        private IntegrationMongoDbSettings _settings;
        private MongoClient _client;
        private IMongoDatabase _server;

        [TestFixtureSetUp]
        public void Setup()
        {
            _settings = new IntegrationMongoDbSettings();
            _client = new MongoClient(_settings.ConnectionString);
            _server = _client.GetDatabase(_settings.Database);
        }

        [Test]
        public void SettingsCanBeResolved()
        {
            var settings = _server.Settings.ToJson();
            Assert.That(settings, Is.Not.Null);
            Console.WriteLine("mongo settings: {0}", settings);
        }
    }

    [TestFixture]
   public class ApplicationRepositoryTests
    {
        private ApplicationState _applicationToSave;
        private ApplicatonRepository _objectUnderTest;

        [TestFixtureSetUp]
        public void Setup()
        {
            GivenProperMongoDatabaseSetupForIntegrationTests();
            _objectUnderTest =
                new ApplicatonRepository(new IntegrationMongoDbSettings());
        }

        [Test]
        public void SaveApplicationSaves()
        {
            GivenAValidApplicationToSave();
            WhenISaveTheApplication(_applicationToSave);
        }

        private void WhenISaveTheApplication(ApplicationState applicationToSave)
        {
            
        }

        private void GivenAValidApplicationToSave()
        {
            _applicationToSave = new ApplicationState()
            {
                Applicant = new Applicant("Daniel", "Lillja"),
                ApplicationStatus = new ApplicationStatus().Accepted(),
                Id = Guid.NewGuid(),
                Offer = new CreditOffer().DefaultOffer()
            };
        }

        private void GivenProperMongoDatabaseSetupForIntegrationTests()
        {
            
        }
    }

    public class IntegrationMongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString
        {
            get { return "mongodb://localhost"; }
        }

        public string Database
        {
            get { return "DDDWorkshopIntegrationTest"; }
        }

        public string ApplicationCollectionName
        {
            get { return "Applications"; }
        }
    }

    public abstract class MongoDataIntegrationTestBase
    {
    }
}
