using System;
using MongoRepository;

namespace DDD_Workshop.Data
{
    public class DomainStateRepository<TDataModel>
        : MongoRepository<TDataModel, Guid> where TDataModel : IEntity<Guid>

    {
        public DomainStateRepository(IMongoDbSettings dbSettings)
            : base(dbSettings.ConnectionString)
        {

        }
    }
}
