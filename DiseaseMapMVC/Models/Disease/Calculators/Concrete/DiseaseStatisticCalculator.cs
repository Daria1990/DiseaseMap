using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    /// <summary>
    /// Класс для рассчета статистики заболеваемости
    /// </summary>
    public class DiseaseStatisticCalculator : IDiseaseStatisticCalculator
    {
        /// <summary>
        /// Меры принимаемые для борьбы с инфекцией
        /// </summary>
        private IMeasuresEfficiency _MeasuresEfficiency;

        /// <summary>
        /// Кампания по вакцинации
        /// </summary>
        private IVaccinationCompany _VaccinationCompany;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="measuresEfficiency">Меры принимаемые для борьбы с инфекцией</param>
        /// <param name="vaccinationCompany">Кампания по вакцинации</param>
        public DiseaseStatisticCalculator(IMeasuresEfficiency measuresEfficiency, IVaccinationCompany vaccinationCompany)
        {
            _MeasuresEfficiency = measuresEfficiency ?? throw new ArgumentNullException(nameof(measuresEfficiency)); 
            _VaccinationCompany = vaccinationCompany ?? throw new ArgumentNullException(nameof(vaccinationCompany));
        }

        /// <summary>
        /// Метод рассчитывает статистику заболеваемости за текущий день
        /// </summary>
        /// <param name="diseaseCitySpread">Распространяемость заболевания</param>
        /// <param name="cityEpidemic">Эпидемия в городе</param>
        /// <param name="city">Параметры города</param>
        /// <returns>Статистика за сегодняшний день</returns>
        public DiseaseStatistic CalculateForToday(DiseaseCitySpread diseaseCitySpread, CityEpidemic cityEpidemic, City city)
        {
            var dead = diseaseCitySpread.CalculateDead();

            var peopleInRiskProcent = _MeasuresEfficiency.GetPeopleInRickProcent(cityEpidemic.Countermeasures, city.Population);
            var sick = diseaseCitySpread.CalculateSick(peopleInRiskProcent);

            var recovered = diseaseCitySpread.CalculateRecovered();

            var injured = diseaseCitySpread.CalculateInjured(_VaccinationCompany, cityEpidemic.VaccinesDoneDayily);

            return new DiseaseStatistic 
            { 
                Dead = dead, 
                Sick = sick, 
                Recovered = diseaseCitySpread.CalculateRecovered(), 
                Injured = injured 
            };
        }
    }
}
