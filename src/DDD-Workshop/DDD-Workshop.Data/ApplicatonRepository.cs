using System;
using System.Collections.Generic;
using DDD_Workshop.Domain;
using MongoDB.Driver;

namespace DDD_Workshop.Data
{
    public class ApplicatonRepository : IApplicationRepository
    {
        private readonly IMongoDbSettings _mongoSettings;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        public ApplicatonRepository(IMongoDbSettings mongoSettings)
        {
            _mongoSettings = mongoSettings;
            _mongoClient = new MongoClient(_mongoSettings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_mongoSettings.Database);
        }

        public List<ApplicationState> GetApplicationsWithApplicant(Applicant applicant)
        {
            throw new NotImplementedException();
        }

        public void SaveApplication(ApplicationState application)
        {
            var collection = _mongoDatabase.GetCollection<Applicant>(_mongoSettings.ApplicationCollectionName);
            var existingApplicant = collection.FindAsync(applicant => applicant.Equals(application.Applicant));

        }

        public ApplicationState GetApplicationById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
