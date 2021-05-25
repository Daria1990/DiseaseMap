using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiseaseMapMVC.Models.ViewModels;
using DiseaseMongoModel;
using MongoDB.Bson;

namespace DiseaseMapMVC.Models.Converter
{
    public class CityMongoToViewModelConverter<TEntity, TViewModel> 
                            : IMongoViewModelConverter<TEntity, TViewModel> 
                            where TEntity : City
                            where TViewModel : CityViewModel
    {
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

        public TEntity Convert(TViewModel viewModel, TEntity entity)
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

            return entity;
        }
    }
}
