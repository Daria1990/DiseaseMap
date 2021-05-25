using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    public class VaccinationCompany : IVaccinationCompany
    {
        private int _vaccinesDoneDayily;
        public VaccinationCompany(int vaccinesDoneDayily)
        {
            _vaccinesDoneDayily = vaccinesDoneDayily;
        }

        public int GetTodaysVacciantedNumber(int peopleNeedVaccination)
        {
            if (_vaccinesDoneDayily > peopleNeedVaccination)
            {
                return peopleNeedVaccination;
            }
            else
            {
                return _vaccinesDoneDayily;
            }
        }
    }
}
