using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using DDD_Workshop.Data;
using Microsoft.Owin.Testing;
using MongoDB.Driver;
using Newtonsoft.Json;
using NUnit.Framework;
using StructureMap;

namespace DDD_Workshop.IntegrationTests.Api
{
    public class RestIntegrationTests
    {
        public IContainer Container { get; set; }
        protected TestServer Server;

        [TestFixtureSetUp]
        public void Setup()
        {
            ContainerBootstrapper.Bootstrap();
            Container = ObjectFactory.Container;
            // create in memory http server
            Server = TestServer.Create<Startup>();
        }

        [SetUp]
        public void ClearDatabaseForTest()
        {
            var dbSettings = Container.GetInstance<IMongoDbSettings>();
            var database = new MongoClient(dbSettings.ConnectionString);
            database.GetServer().DropDatabase(dbSettings.Database);
        }

        protected static string LoadJsonFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return json;
        }

        protected HttpResponseMessage PostJsonFromFile(string filePath)
        {
            var json = LoadJsonFromFile(filePath);

            var response =
                Server.HttpClient.PostAsync("/api/applications", new StringContent(json), new JsonMediaTypeFormatter()).Result;
            return response;
        }

        protected static void ThenServerResponseIsSuccessful(HttpResponseMessage response)
        {
            Assert.That(response.EnsureSuccessStatusCode().IsSuccessStatusCode, Is.True);
        }

        protected static void WriteServerResponseToConsole(HttpResponseMessage response)
        {
            Console.WriteLine(GetResponseJson(response));
        }

        public static string GetResponseJson(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }

        protected static T DeserializeJsonToDynamicObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Server.Dispose();
        }
    }
}