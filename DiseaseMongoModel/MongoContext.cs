using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    public class MongoContext
    {
        private IMongoDatabase database;

        public MongoContext(string connectionString)
        {
            database = GetDatabase(connectionString);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : BaseEntity
        {
            return database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        private IMongoDatabase GetDatabase(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);

            var client = new MongoClient(connectionString);

            return client.GetDatabase(connection.DatabaseName);
        }
    }
}
