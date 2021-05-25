using DiseaseMapMVC.Models.ViewModels;
using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Converter
{
    public interface IMongoViewModelConverter<TEntity, TViewModel>
    {
        public TViewModel Convert(TEntity entity);

        public TEntity Convert(TViewModel entity);

        public TEntity Convert(TViewModel viewModel, TEntity entity);
    }
}
