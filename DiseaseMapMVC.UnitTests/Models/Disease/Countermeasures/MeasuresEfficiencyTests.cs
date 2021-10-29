using DiseaseMapMVC.Models.Disease;
using DiseaseMongoModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{ 
    [TestFixture]
    public class MeasuresEfficiencyTests
    {
        private readonly int _MaxNumber = 1000;

        private int GetNumber()
        {
            var random = new Random();
            return random.Next(1, _MaxNumber);
        }

        [Test]
        public void GetPeopleInRickProcent_EmptyCity_ReturnOne()
        {
            CityPopulation cityPopulation = null;

            var measuresEfficiency = new MeasuresEfficiency();
            var peopleInRickProcent = measuresEfficiency.GetPeopleInRickProcent(null, cityPopulation);

            Assert.AreEqual(peopleInRickProcent, 1);
        }

        [Test]
        public void GetPeopleInRickProcent_KinderGartensIsClosedThenKidsOutOfRisk_ReturnZero()
        {
            var cityPopulation = new CityPopulation { KidNumber = GetNumber() };
            var countermeasures = new Countermeasures { CloseKinderGartens = true };

            var measuresEfficiency = new MeasuresEfficiency();
            var peopleInRickProcent = measuresEfficiency.GetPeopleInRickProcent(countermeasures, cityPopulation);

            Assert.Zero(peopleInRickProcent);
        }

        [Test]
        public void GetPeopleInRickProcent_RemoteWorkThenAdultsOutOfRisk_ReturnZero()
        {
            var cityPopulation = new CityPopulation { AdultNumber = GetNumber() };
            var countermeasures = new Countermeasures { RemoteWork = true };

            var measuresEfficiency = new MeasuresEfficiency();
            var peopleInRickProcent = measuresEfficiency.GetPeopleInRickProcent(countermeasures, cityPopulation);

            Assert.Zero(peopleInRickProcent);
        }

        [Test]
        public void GetPeopleInRickProcent_CloseSchoolsThenAdolescentOutOfRisk_ReturnZero()
        {
            var cityPopulation = new CityPopulation { AdolescentNumber = GetNumber() };
            var countermeasures = new Countermeasures { CloseSchools = true };

            var measuresEfficiency = new MeasuresEfficiency();
            var peopleInRickProcent = measuresEfficiency.GetPeopleInRickProcent(countermeasures, cityPopulation);

            Assert.Zero(peopleInRickProcent);
        }

        [Test]
        public void GetPeopleInRickProcent_StopTransportThenRetireeOutOfRisk_ReturnZero()
        {
            var cityPopulation = new CityPopulation { RetireeNumber = GetNumber() };
            var countermeasures = new Countermeasures { StopTransport = true };

            var measuresEfficiency = new MeasuresEfficiency();
            var peopleInRickProcent = measuresEfficiency.GetPeopleInRickProcent(countermeasures, cityPopulation);

            Assert.Zero(peopleInRickProcent);
        }
    }
}
