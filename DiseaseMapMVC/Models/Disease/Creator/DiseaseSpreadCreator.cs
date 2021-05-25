using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    using DiseaseMongoModel;

    public class DiseaseSpreadCreator
    {
        /// <summary>
        /// Метод создает класс распространения болезни в городе
        /// </summary>
        /// <param name="disease">болезнь</param>
        /// <param name="diseaseStatistics">статистика эпидемии</param>
        /// <param name="cityPeopleNumber">количество жителей в городе</param>
        /// <returns>класс расчета распространения болезни</returns>
        public DiseaseCitySpread CreateForCity(Disease disease, List<DiseaseStatistic> diseaseStatistics, int cityPeopleNumber)
        {
            if (disease == null || string.IsNullOrEmpty(disease.MKBCode))
            {
                throw new ArgumentNullException("MKBCode", "MKBCode of Disease is not specified.");
            }

            var mkbCode = disease.MKBCode.ToUpper();

            if (mkbCode == "B05")
            {
                return new MeaslesCitySpread(disease, diseaseStatistics, cityPeopleNumber);
            }
            else if (mkbCode == "U07.1")
            {
                return new CoronavirusCitySpread(disease, diseaseStatistics, cityPeopleNumber);
            }
            else
            {
                throw new ArgumentOutOfRangeException("MKBCode", "MKBCode of Disease is not correct.");
            }
        }
    }
}
