using DiseaseMapMVC.Models;
using DiseaseMongoModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    [TestFixture]
    public class CountryEidemicParameterSetterTests
    {
        private List<City> _cities = new List<City>
        {
            new City { Id = Guid.NewGuid().ToString(), Name = "City1"},
            new City { Id = Guid.NewGuid().ToString(), Name = "City2" }
        };

        [Test]
        public void SetCities_CityEpidemicsAddedForCities_CityEpidemicCountIsEqualToCityCount()
        {
            var countryEpidemic = new CountryEpidemic();

            var setter = new CountryEidemicParameterSetter(countryEpidemic);
            setter.SetCities(_cities);

            Assert.IsTrue(_cities.Count == countryEpidemic.CityEpidemics.Count);
        }
    }
}
