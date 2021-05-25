using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiseaseMapMVC.Models.ViewModels;

namespace DiseaseMapMVC.Models.Converter
{
    public class CountryMongoToViewModelConverter<TEntity, TViewModel>
                                        : IMongoViewModelConverter<TEntity, TViewModel>
                                        where TEntity : Country
                                        where TViewModel : CountryViewModel
    {
        public TViewModel Convert(TEntity entity)
        {
            return (TViewModel)new CountryViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public TEntity Convert(TViewModel viewModel)
        {
            return (TEntity)new Country
            {
                Name = viewModel.Name
            };
        }

        public TEntity Convert(TViewModel viewModel, TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Name = viewModel.Name;

            return entity;
        }
    }
}
