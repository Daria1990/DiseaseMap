using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    using DiseaseMongoModel;

    /// <summary>
    /// Интерфейс фабрики создания классов для рассчета распространения заболевания
    /// </summary>
    public interface IDiseaseSpreadFactory
    {
        /// <summary>
        /// Метод создает класс распространения болезни в городе
        /// </summary>
        /// <param name="disease">болезнь</param>
        /// <param name="diseaseStatistics">статистика эпидемии</param>
        /// <param name="population">количество жителей в городе</param>
        /// <returns>класс расчета распространения болезни</returns>
        DiseaseCitySpread Create(Disease disease, List<DiseaseStatistic> diseaseStatistics, int population);
    }
}
