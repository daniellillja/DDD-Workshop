using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace DDD_Workshop.IntegrationTests.Api
{
    [TestFixture]
    public class ApplicationControllerIntegrationTests
    {
        private TestServer _server;

        [TestFixtureSetUp]
        public void Setup()
        {
            _server = TestServer.Create<Startup>();
        }

        [Test]
        public void ValidRequestWithFirstAndLastName()
        {
            var filePath = @"Api\JsonData\ValidRequestWithFirstAndLastName.txt";
            var json = File.ReadAllText(filePath);
            var response = _server.HttpClient.PostAsync("/api/application",
                new StringContent(json)).Result;
            Assert.That(response.EnsureSuccessStatusCode(), Is.True);
            
            Debug.WriteLine(response);

        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _server.Dispose();
        }
    }

    

}
