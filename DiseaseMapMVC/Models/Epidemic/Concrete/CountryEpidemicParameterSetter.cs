using DiseaseMongoModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Epidemic
{
    /// <summary>
    /// Класс устанавливает значения полей для эпидемии
    /// </summary>
    public class CountryEpidemicParameterSetter : ICountryEpidemicParameterSetter
    {
        /// <summary>
        /// Метод проставляет города, которые может затронуть эпидемия
        /// </summary>
        /// <param name="countryEpidemic">Эпидемия в стране</param>
        /// <param name="cities">список городов, в которых возможна эпидемия</param>
        public void SetCities(CountryEpidemic countryEpidemic, ICollection<City> cities)
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

            countryEpidemic.CityEpidemics = cityEpidemics;
            countryEpidemic.DayFromStart = 1;
        }

        /// <summary>
        /// Метод возвращает ситуацию в городах, которые затронула эпидемия на стартовые значения
        /// </summary>
        /// <param name="countryEpidemic">Эпидемия в стране</param>
        public void ResetCities(CountryEpidemic countryEpidemic)
        {
            for (int i = 0; i < countryEpidemic.CityEpidemics.Count; i++)
            {
                var initialCityStatistic = countryEpidemic.CityEpidemics[i].DiseaseStatistics[0];

                countryEpidemic.CityEpidemics[i].DiseaseStatistics = new List<DiseaseStatistic> { initialCityStatistic };
            }

            countryEpidemic.DayFromStart = 1;
        }
    }
}
