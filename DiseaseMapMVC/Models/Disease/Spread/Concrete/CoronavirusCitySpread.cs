using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    using DiseaseMongoModel;

    /// <summary>
    /// Распространение коронавируса в городе
    /// </summary>
    public class CoronavirusCitySpread : DiseaseCitySpread
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="disease">Заболевание</param>
        /// <param name="diseaseStatistics">Статистика заболевания</param>
        /// <param name="cityPopulation">количество людей в городе</param>
        public CoronavirusCitySpread(Disease disease, List<DiseaseStatistic> diseaseStatistics, int cityPeopleNumber) :
            base(disease, diseaseStatistics, cityPeopleNumber) { }


        protected override double CalculateRe()
        {
            if (SpreadStatistics.Count() >= ConstantValues.DefaultCoronavirusDaysSpreadCount)
            {
                var lastDaysSicks = SpreadStatistics.Skip(SpreadStatistics.Count() - ConstantValues.DefaultCoronavirusDaysSpreadCount)
                                    .Select(s=>s.Sick).ToArray();


                var first4Days = lastDaysSicks[3] + lastDaysSicks[2] + lastDaysSicks[1] + lastDaysSicks[0];

                if (first4Days == 0)
                {
                    return 0;
                }
                else
                {
                    return (lastDaysSicks[7] + lastDaysSicks[6] + lastDaysSicks[5] + lastDaysSicks[4]) / first4Days;
                }
            }
            else 
            {
                return base.CalculateRe();
            }
        }
    }
}
