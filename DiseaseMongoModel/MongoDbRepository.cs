using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Репозиторий для заданного типа сущности в Mongo
    /// </summary>
    /// <typeparam name="TEntity">тип сущности</typeparam>
    public class MongoDbRepository<TEntity> : IRepository<TEntity>, ISearch<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Коллекция элементов в таблице
        /// </summary>
        private IMongoCollection<TEntity> _Collection;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mongoContext">контекст Mongo</param>
        public MongoDbRepository(MongoContext mongoContext)
        {
            _Collection = mongoContext.GetCollection<TEntity>();
        }

        /// <summary>
        /// Метод создает новую запись в таблице
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>корректно ли была создана запись</returns>
        public bool Create(TEntity entity)
        {
            try
            {
                _Collection.InsertOne(entity);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Метод создает новую запись в таблице
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id">id созданной записи</param>
        /// <returns>корректно ли была создана запись</returns>
        public bool Create(TEntity entity, out string id)
        {
            try
            {
                _Collection.InsertOne(entity);

                id = entity.Id;

                return true;
            }
            catch (Exception)
            {
                id = null;

                return false;
            }
        }

        /// <summary>
        /// Метод редактирует запись в таблице
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>корректно ли была отредактирована запись</returns>
        public bool Update(TEntity entity)
        {
            var filterId = Builders<TEntity>.Filter.Eq("_id", new ObjectId(entity.Id));

            var updated = _Collection.FindOneAndReplace(filterId, entity);

            return updated != null;
        }

        /// <summary>
        ///  Метод удаляет запись из таблицы
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>корректно ли была удалена запись</returns>
        public bool Delete(TEntity entity)
        {
            var filterId = Builders<TEntity>.Filter.Eq("_id", new ObjectId(entity.Id));

            var deleted = _Collection.FindOneAndDelete(filterId);

            return deleted != null;
        }

        /// <summary>
        /// Метод ищет записи по заданному условию
        /// </summary>
        /// <param name="filter">условие, по которому фильтруют записи</param>
        /// <returns></returns>
        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> filter)
        {
            return _Collection.Find(filter).ToList();
        }

        /// <summary>
        /// Метод возвращает все записи в таблице
        /// </summary>
        /// <returns>все записи в таблице</returns>
        public IList<TEntity> SearchAll()
        {
            return _Collection.Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// Метод получает запись по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>запись с заданной id</returns>
        public TEntity GetById(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));

            return _Collection.Find(filter).FirstOrDefault();
        }
    }
}
