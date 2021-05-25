using DiseaseMapMVC.Models.Disease;
using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    public class FakeCitySpread : DiseaseCitySpread
    {
        public FakeCitySpread(Disease disease, List<DiseaseStatistic> diseaseStatistics, int peopleNumber)
            : base(disease, diseaseStatistics, peopleNumber) { }

        public new double CalculateRe()
        {
            return base.CalculateRe();
        }

        public new int GetCanBecomeSick()
        {
            return base.GetCanBecomeSick();
        }

        public new int GetSickInDurationDay()
        {
            return base.GetSickInDurationDay();
        }

        public new int GetDeadBeforeToday()
        {
            return base.GetDeadBeforeToday();
        }

        public new int CheckForOverload(int check)
        {
            return base.CheckForOverload(check);
        }
    }
}
