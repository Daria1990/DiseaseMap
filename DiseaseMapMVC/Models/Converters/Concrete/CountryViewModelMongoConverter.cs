using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiseaseMapMVC.Models.ViewModels;

namespace DiseaseMapMVC.Models.Converter
{
    /// <summary>
    /// Класс для взаимного преобразования модели представления страны и сущности для работы с Mongo
    /// </summary>
    /// <typeparam name="TEntity">сущность для работы с Mongo</typeparam>
    /// <typeparam name="TViewModel">модель представления</typeparam>
    public class CountryMongoToViewModelConverter<TEntity, TViewModel>
                                        : IMongoViewModelConverter<TEntity, TViewModel>
                                        where TEntity : Country
                                        where TViewModel : CountryViewModel
    {
        /// <summary>
        /// Метод преобразует сущность для работы с Mongo в модель представления
        /// </summary>
        /// <param name="entity">сущность для работы с Mongo</param>
        /// <returns>модель представления</returns>
        public TViewModel Convert(TEntity entity)
        {
            return (TViewModel)new CountryViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        /// <summary>
        /// Метод преобразует модель представления в сущность для работы с Mongo 
        /// </summary>
        /// <param name="viewModel">модель представления</param>
        /// <returns>сущность для работы с Mongo</returns>
        public TEntity Convert(TViewModel viewModel)
        {
            return (TEntity)new Country
            {
                Name = viewModel.Name
            };
        }

        /// <summary>
        /// Метод дополняет сущность для работы с Mongo данными из модели представления
        /// </summary>
        /// <param name="viewModel">модель представления</param>
        /// <param name="entity">сущность для работы с Mongo</param>
        public void Convert(TViewModel viewModel, TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Name = viewModel.Name;
        }
    }
}
