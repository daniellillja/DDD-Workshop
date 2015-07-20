using System;
using MongoRepository;

namespace DDD_Workshop.Data
{
    public class DomainStateRepositoryManager<TDataModel>
        : MongoRepositoryManager<TDataModel, Guid>
        where TDataModel : IEntity<Guid>
    {
        public DomainStateRepositoryManager(IMongoDbSettings dbSettings)
            : base(dbSettings.ConnectionString)
        {

        }
    }
}