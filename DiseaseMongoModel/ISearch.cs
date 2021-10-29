using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Интерфейс поиска данных в таблице
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISearch<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Метод ищет записи по заданному условию
        /// </summary>
        /// <param name="filter">условие, по которому фильтруют записи</param>
        /// <returns></returns>
        IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Метод возвращает все записи в таблице
        /// </summary>
        /// <returns>все записи в таблице</returns>
        IList<TEntity> SearchAll();
    }
}
