using DiseaseMapMVC.Models.Converter;
using DiseaseMapMVC.Models.ViewModels;
using DiseaseMongoModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    [TestFixture]
    public class CountryMongoToViewModelConverterTests: MongoViewModelConverterTests<Country, CountryViewModel>
    {
        protected override IMongoViewModelConverter<Country, CountryViewModel> GetConverter()
        {
            return new CountryMongoToViewModelConverter<Country, CountryViewModel>();
        }
    }
}
