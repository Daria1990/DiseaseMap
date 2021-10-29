using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Класс контекста Mongo
    /// </summary>
    public class MongoContext
    {
        /// <summary>
        /// База данных в Mongo
        /// </summary>
        private IMongoDatabase _Database;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="connectionString">строка подключения</param>
        public MongoContext(string connectionString)
        {
            _Database = GetDatabase(connectionString);
        }

        /// <summary>
        /// Метод получает данные из таблицы Mongo
        /// </summary>
        /// <typeparam name="TEntity">тип сущности, для которого надо получить данные</typeparam>
        /// <returns>данные из таблицы</returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : BaseEntity
        {
            return _Database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        /// <summary>
        /// Метод получает базу данных в Mongo
        /// </summary>
        /// <param name="connectionString">строка подключения к Mongo</param>
        /// <returns>база данных</returns>
        private IMongoDatabase GetDatabase(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);

            var client = new MongoClient(connectionString);

            return client.GetDatabase(connection.DatabaseName);
        }
    }
}
