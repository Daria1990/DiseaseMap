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
    public class CityMongoToViewModelConverterTests : MongoViewModelConverterTests<City, CityViewModel>
    { 
        protected override IMongoViewModelConverter<City, CityViewModel> GetConverter()
        {
            return new CityMongoToViewModelConverter<City, CityViewModel>();
        }

        [Test]
        public void ConvertViewModelToEntity_CountryIdParameterIsNull_ThrowException()
        {
            var viewModel = new CityViewModel { CountryId = null };

            var converter = GetConverter();

            Assert.Catch<ArgumentNullException>(() => converter.Convert(viewModel));
        }
    }
}