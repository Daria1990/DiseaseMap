using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using DiseaseMapMVC.Models.Disease;

namespace DiseaseMapMVC.UnitTests
{
    [TestFixture]
    public class VaccinationCompanyTests
    {
        private readonly int MAX_VACCIENS_DONE_DAYILY = 10000;
        private int GetVaccinesDoneDayily()
        {
            var random = new Random();
            return random.Next(1, MAX_VACCIENS_DONE_DAYILY);
        }

        [Test]
        public void GetTodaysVacciantedNumber_ExcessDayVaccination_ReturnPeopleNeedVacciantion()
        {
            var vaccinesDoneDayily = GetVaccinesDoneDayily();
            var peopleNeedVaccination = vaccinesDoneDayily - 1;

            var vaccinationCompany = new VaccinationCompany(vaccinesDoneDayily);
            var vaccinatedToday = vaccinationCompany.GetTodaysVacciantedNumber(peopleNeedVaccination);

            Assert.AreEqual(vaccinatedToday, peopleNeedVaccination);
        }

        [Test]
        public void GetTodaysVacciantedNumber_DeficientDayVaccination_ReturnVaccinesDoneDayily()
        {
            var vaccinesDoneDayily = GetVaccinesDoneDayily();
            var peopleNeedVaccination = vaccinesDoneDayily + 1;

            var vaccinationCompany = new VaccinationCompany(vaccinesDoneDayily);
            var vaccinatedToday = vaccinationCompany.GetTodaysVacciantedNumber(peopleNeedVaccination);

            Assert.AreEqual(vaccinatedToday, vaccinesDoneDayily);
        }
    }
}
