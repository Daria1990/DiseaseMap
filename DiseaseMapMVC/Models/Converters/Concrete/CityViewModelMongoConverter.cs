using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiseaseMapMVC.Models.ViewModels;
using DiseaseMongoModel;
using MongoDB.Bson;

namespace DiseaseMapMVC.Models.Converter
{
    /// <summary>
    /// Класс для взаимного преобразования модели представления города и сущности для работы с Mongo
    /// </summary>
    /// <typeparam name="TEntity">сущность для работы с Mongo</typeparam>
    /// <typeparam name="TViewModel">модель представления</typeparam>
    public class CityMongoToViewModelConverter<TEntity, TViewModel> 
                            : IMongoViewModelConverter<TEntity, TViewModel> 
                            where TEntity : City
                            where TViewModel : CityViewModel
    {
        /// <summary>
        /// Метод преобразует сущность для работы с Mongo в модель представления
        /// </summary>
        /// <param name="entity">сущность для работы с Mongo</param>
        /// <returns>модель представления</returns>
        public TViewModel Convert(TEntity entity)
        {
            return (TViewModel)new CityViewModel
            {
                Id = entity.Id,
                CountryId = entity.CountryId.ToString(),
                Name = entity.Name,
                KidNumber = entity.Population.KidNumber,
                AdolescentNumber = entity.Population.AdolescentNumber,
                AdultNumber = entity.Population.AdultNumber,
                RetireeNumber = entity.Population.RetireeNumber
            };
        }

        /// <summary>
        /// Метод преобразует модель представления в сущность для работы с Mongo 
        /// </summary>
        /// <param name="viewModel">модель представления</param>
        /// <returns>сущность для работы с Mongo</returns>
        public TEntity Convert(TViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.CountryId))
            {
                throw new ArgumentNullException("viewModel.CountryId");
            }

            return (TEntity)new City
            {
                Name = viewModel.Name,
                CountryId = new ObjectId(viewModel.CountryId),
                Population = new CityPopulation
                {
                    KidNumber = viewModel.KidNumber,
                    AdolescentNumber = viewModel.AdolescentNumber,
                    AdultNumber = viewModel.AdultNumber,
                    RetireeNumber = viewModel.RetireeNumber
                }
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
            entity.Population.KidNumber = viewModel.KidNumber;
            entity.Population.AdolescentNumber = viewModel.AdolescentNumber;
            entity.Population.AdultNumber = viewModel.AdultNumber;
            entity.Population.RetireeNumber = viewModel.RetireeNumber;
        }
    }
}
