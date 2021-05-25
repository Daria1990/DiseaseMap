using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    using DiseaseMongoModel;

    public class MeaslesCitySpread : DiseaseCitySpread
    {
        public MeaslesCitySpread(Disease disease, List<DiseaseStatistic> diseaseStatistics, int cityPeopleNumber) :
            base(disease, diseaseStatistics, cityPeopleNumber) { }
    }
}
