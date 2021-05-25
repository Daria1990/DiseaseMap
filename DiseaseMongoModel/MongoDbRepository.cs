using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    public class MongoDbRepository<TEntity> : IRepository<TEntity>, ISearch<TEntity> where TEntity : BaseEntity
    {
        private IMongoCollection<TEntity> collection;

        public MongoDbRepository(MongoContext mongoContext)
        {
            collection = mongoContext.GetCollection<TEntity>();
        }

        public bool Create(TEntity entity)
        {
            try
            {
                collection.InsertOne(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Create(TEntity entity, out string id)
        {
            try
            {
                collection.InsertOne(entity);
                id = entity.Id;
                return true;
            }
            catch (Exception ex)
            {
                id = null;
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            var filterId = Builders<TEntity>.Filter.Eq("_id", new ObjectId(entity.Id));
            var updated = collection.FindOneAndReplace(filterId, entity);

            return updated != null;
        }

        public bool Delete(TEntity entity)
        {
            var filterId = Builders<TEntity>.Filter.Eq("_id", new ObjectId(entity.Id));
            var deleted = collection.FindOneAndDelete(filterId);

            return deleted != null;
        }

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> filter)
        {
            return collection.Find(filter).ToList();
        }

        public IList<TEntity> SearchAll()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public TEntity GetById(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
            return collection.Find(filter).FirstOrDefault();
        }
    }
}
