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
        private readonly int _MaxVacciensDoneDaily = 10000;

        private int GetVaccinesDoneDayily()
        {
            var random = new Random();
            return random.Next(1, _MaxVacciensDoneDaily);
        }

        [Test]
        public void GetTodaysVaccianted_ExcessDayVaccination_ReturnPeopleNeedVacciantion()
        {
            var vaccinesDoneDayily = GetVaccinesDoneDayily();
            var peopleNeedVaccination = vaccinesDoneDayily - 1;

            var vaccinationCompany = new VaccinationCompany();
            var vaccinatedToday = vaccinationCompany.GetTodaysVaccianted(vaccinesDoneDayily, peopleNeedVaccination);

            Assert.AreEqual(vaccinatedToday, peopleNeedVaccination);
        }

        [Test]
        public void GetTodaysVaccianted_DeficientDayVaccination_ReturnVaccinesDoneDayily()
        {
            var vaccinesDoneDayily = GetVaccinesDoneDayily();
            var peopleNeedVaccination = vaccinesDoneDayily + 1;

            var vaccinationCompany = new VaccinationCompany();
            var vaccinatedToday = vaccinationCompany.GetTodaysVaccianted(vaccinesDoneDayily, peopleNeedVaccination);

            Assert.AreEqual(vaccinatedToday, vaccinesDoneDayily);
        }
    }
}
