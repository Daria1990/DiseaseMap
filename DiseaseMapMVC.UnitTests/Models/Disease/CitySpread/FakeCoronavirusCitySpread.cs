using DiseaseMapMVC.Models.Disease;
using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    public class FakeCoronavirusCitySpread : CoronavirusCitySpread
    {
        public FakeCoronavirusCitySpread(Disease disease, List<DiseaseStatistic> diseaseStatistics, int peopleNumber)
            : base(disease, diseaseStatistics, peopleNumber) { }

        public new double CalculateRe()
        {
            return base.CalculateRe();
        }
    }
}
