using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    using DiseaseMongoModel;

    /// <summary>
    /// Распространение кори в городе
    /// </summary>
    public class MeaslesCitySpread : DiseaseCitySpread
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="disease">Заболевание</param>
        /// <param name="diseaseStatistics">Статистика заболевания</param>
        /// <param name="cityPopulation">количество людей в городе</param>
        public MeaslesCitySpread(Disease disease, List<DiseaseStatistic> diseaseStatistics, int cityPeopleNumber) :
            base(disease, diseaseStatistics, cityPeopleNumber) { }
    }
}
