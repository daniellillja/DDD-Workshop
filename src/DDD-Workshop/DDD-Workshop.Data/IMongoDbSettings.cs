using MongoDB.Driver;

namespace DDD_Workshop.Data
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; }
        string Database { get; }
        string ApplicationCollectionName { get; }
    }
}
