using DiseaseMapMVC.Models.ViewModels;
using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Converter
{
    /// <summary>
    /// Интерфейс для взаимного преобразования модели представления страны и сущности для работы с Mongo
    /// </summary>
    /// <typeparam name="TEntity">сущность для работы с Mongo</typeparam>
    /// <typeparam name="TViewModel">модель представления</typeparam>
    public interface IMongoViewModelConverter<TEntity, TViewModel>
    {
        /// <summary>
        /// Метод преобразует сущность для работы с Mongo в модель представления
        /// </summary>
        /// <param name="entity">сущность для работы с Mongo</param>
        /// <returns>модель представления</returns>
        TViewModel Convert(TEntity entity);

        /// <summary>
        /// Метод преобразует модель представления в сущность для работы с Mongo 
        /// </summary>
        /// <param name="viewModel">модель представления</param>
        /// <returns>сущность для работы с Mongo</returns>
        TEntity Convert(TViewModel entity);

        /// <summary>
        /// Метод дополняет сущность для работы с Mongo данными из модели представления
        /// </summary>
        /// <param name="viewModel">модель представления</param>
        /// <param name="entity">сущность для работы с Mongo</param>
        void Convert(TViewModel viewModel, TEntity entity);
    }
}
