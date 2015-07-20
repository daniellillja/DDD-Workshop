using System;
using DDD_Workshop.Data;

namespace DDD_Workshop.WebApi
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString
        {
            get { return "mongodb://localhost/dddworkshop"; }
        }

        public string Database
        {
            get { throw new NotImplementedException(); }
        }
    }
}