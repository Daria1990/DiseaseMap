using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    using DiseaseMapMVC.Resources.Models;
    using DiseaseMongoModel;

    /// <summary>
    /// Фабрика создания классов для рассчета распространения заболевания
    /// </summary>
    public class DiseaseSpreadFactory : IDiseaseSpreadFactory
    {
        /// <summary>
        /// Метод создает класс распространения болезни в городе
        /// </summary>
        /// <param name="disease">болезнь</param>
        /// <param name="diseaseStatistics">статистика эпидемии</param>
        /// <param name="population">количество жителей в городе</param>
        /// <returns>класс расчета распространения болезни</returns>
        public DiseaseCitySpread Create(Disease disease, List<DiseaseStatistic> diseaseStatistics, int population)
        {
            if (disease == null || string.IsNullOrEmpty(disease.MKBCode))
            {
                throw new ArgumentNullException(DiseaseMKBMessages.EmptyMKBCode);
            }

            var mkbCode = disease.MKBCode.ToUpper();

            if (mkbCode == DiseaseMKBMessages.Measles)
            {
                return new MeaslesCitySpread(disease, diseaseStatistics, population);
            }
            else if (mkbCode == DiseaseMKBMessages.Coronavirus)
            {
                return new CoronavirusCitySpread(disease, diseaseStatistics, population);
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format(DiseaseMKBMessages.IncorrectMKBCode, mkbCode));
            }
        }
    }
}
