using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models
{
    public class CountryEidemicParameterSetter
    {
        private CountryEpidemic _countryEpidemic;

        public CountryEidemicParameterSetter(CountryEpidemic countryEpidemic)
        {
            _countryEpidemic = countryEpidemic;
        }

        public CountryEpidemic SetCities(IEnumerable<City> cities)
        {
            var cityEpidemics = new List<CityEpidemic>();

            foreach (var city in cities)
            {
                var cityEpidemic = new CityEpidemic
                {
                    CityId = city.Id,
                    CityName = city.Name,
                    DiseaseStatistics = new List<DiseaseStatistic> { new DiseaseStatistic() },
                    Countermeasures = new Countermeasures()
                };

                cityEpidemics.Add(cityEpidemic);
            }

            _countryEpidemic.CityEpidemics = cityEpidemics;
            _countryEpidemic.DayFromStart = 1;

            return _countryEpidemic;
        }

        public CountryEpidemic SetCities()
        {
            for (int i = 0; i < _countryEpidemic.CityEpidemics.Count; i++)
            {
                var initialCityStatistic = _countryEpidemic.CityEpidemics[i].DiseaseStatistics[0];

                _countryEpidemic.CityEpidemics[i].DiseaseStatistics = new List<DiseaseStatistic> { initialCityStatistic };
            }

            _countryEpidemic.DayFromStart = 1;

            return _countryEpidemic;
        }
    }
}
