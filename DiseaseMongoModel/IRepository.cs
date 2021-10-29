using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Интерфейс работы с репозиторием
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Метод создает новую запись в таблице
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>корректно ли была создана запись</returns>
        bool Create(TEntity entity);

        /// <summary>
        /// Метод редактирует запись в таблице
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>корректно ли была отредактирована запись</returns>
        bool Update(TEntity entity);

        /// <summary>
        ///  Метод удаляет запись из таблицы
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>корректно ли была удалена запись</returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// Метод получает запись по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>запись с заданной id</returns>
        TEntity GetById(string id);
    }
}
