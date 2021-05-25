using System;
using System.Collections.Generic;
using System.Text;
using DiseaseMapMVC.Models;
using DiseaseMongoModel;
using NUnit.Framework;

namespace DiseaseMapMVC.UnitTests
{
    [TestFixture]
    public class CoronavirusCitySpreadTests
    {
        private DiseaseCitySpreadParameterSetter _parameterSetter = new DiseaseCitySpreadParameterSetter();

        private int GetDaySickInProposalThatThisNumberIsTheSame(int peopleNumber, int day)
        {
            return peopleNumber / day;
        }

        [Test]
        public void CalculateRe_DidNotSickInFirstFourDays_ReturnsZero()
        {
            var disease = _parameterSetter.CreateDisease();
            var peopleNumber = _parameterSetter.GetPeopleNumber();

            var diseaseStatistics = new List<DiseaseStatistic>();
            for (int i = 0; i < ConstantValues.Disease.DEFAULT_CORONAVIRUS_DAYS_SPREAD_COUNT; i++)
            {
                diseaseStatistics.Add(new DiseaseStatistic { Sick = 0 });
            };

            var citySpread = new FakeCoronavirusCitySpread(disease, diseaseStatistics, peopleNumber);
            var Re = citySpread.CalculateRe();

            Assert.Zero(Re);
        }

        [Test]
        public void CalculateRe_SickInFirstFourDays_ReturnsOne()
        {
            var disease = _parameterSetter.CreateDisease();
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var daySick = GetDaySickInProposalThatThisNumberIsTheSame(peopleNumber, ConstantValues.Disease.DEFAULT_CORONAVIRUS_DAYS_SPREAD_COUNT);

            var diseaseStatistics = new List<DiseaseStatistic>();
            for (int i = 0; i < ConstantValues.Disease.DEFAULT_CORONAVIRUS_DAYS_SPREAD_COUNT; i++)
            {
                diseaseStatistics.Add(new DiseaseStatistic { Sick = daySick });
            };

            var citySpread = new FakeCoronavirusCitySpread(disease, diseaseStatistics, peopleNumber);
            var Re = citySpread.CalculateRe();

            Assert.AreEqual(1, Re);
        }
    }
}
