using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    using DiseaseMongoModel;

    public class CoronavirusCitySpread : DiseaseCitySpread
    {
        public CoronavirusCitySpread(Disease disease, List<DiseaseStatistic> diseaseStatistics, int cityPeopleNumber) :
            base(disease, diseaseStatistics, cityPeopleNumber) { }


        protected override double CalculateRe()
        {
            if (DiseaseStatistics.Count() >= ConstantValues.Disease.DEFAULT_CORONAVIRUS_DAYS_SPREAD_COUNT)
            {
                var lastDaysSicks = DiseaseStatistics.Skip(DiseaseStatistics.Count() - ConstantValues.Disease.DEFAULT_CORONAVIRUS_DAYS_SPREAD_COUNT)
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
