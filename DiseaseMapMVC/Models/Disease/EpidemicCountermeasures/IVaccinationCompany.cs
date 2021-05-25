using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    public interface IVaccinationCompany
    {
        public int GetTodaysVacciantedNumber(int peopleNeedVaccination);
    }
}
