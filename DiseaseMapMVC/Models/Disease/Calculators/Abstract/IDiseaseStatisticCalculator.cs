using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    /// <summary>
    /// Интерфейс для рассчета статистики заболеваемости
    /// </summary>
    public interface IDiseaseStatisticCalculator
    {
        /// <summary>
        /// Метод рассчитывает статистику заболеваемости за текущий день
        /// </summary>
        /// <param name="diseaseCitySpread">Распространяемость заболевания</param>
        /// <param name="cityEpidemic">Эпидемия в городе</param>
        /// <param name="city">Параметры города</param>
        /// <returns>Статистика за сегодняшний день</returns>
        DiseaseStatistic CalculateForToday(DiseaseCitySpread diseaseCitySpread, CityEpidemic cityEpidemic, City city);
    }
}
