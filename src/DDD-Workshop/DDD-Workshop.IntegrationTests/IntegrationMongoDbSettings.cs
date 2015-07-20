using System;
using DDD_Workshop.Data;

namespace DDD_Workshop.IntegrationTests
{
    public class IntegrationMongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString
        {
            get { return "mongodb://localhost/dddworkshop-integrationtests"; }
        }

        public string Database
        {
            get { throw new NotImplementedException(); }
        }
    }
}