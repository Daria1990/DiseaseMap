using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Epidemic
{
    /// <summary>
    /// Интерфейс устанавливает значения полей для эпидемии
    /// </summary>
    public interface ICountryEpidemicParameterSetter
    {
        /// <summary>
        /// Метод проставляет города, которые может затронуть эпидемия
        /// </summary>
        /// <param name="countryEpidemic">Эпидемия в стране</param>
        /// <param name="cities">список городов, в которых возможна эпидемия</param>
        void SetCities(CountryEpidemic countryEpidemic, ICollection<City> cities);

        /// <summary>
        /// Метод возвращает ситуацию в городах, которые затронула эпидемия на стартовые значения
        /// </summary>
        /// <param name="countryEpidemic">Эпидемия в стране</param>
        void ResetCities(CountryEpidemic countryEpidemic);
    }
}
