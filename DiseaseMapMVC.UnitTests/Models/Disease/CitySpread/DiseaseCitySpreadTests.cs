using DiseaseMongoModel;
using NUnit.Framework;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    [TestFixture]
    public class DiseaseCitySpreadTests
    {
        private DiseaseCitySpreadParameterSetter _parameterSetter = new DiseaseCitySpreadParameterSetter();

        [Test]
        public void CalculateRe_YesterdayStatisticsNull_ReturnsBetweenR0MaxAndR0Min()
        {
            var disease = _parameterSetter.CreateDisease();
            var peopleNumber = _parameterSetter.GetPeopleNumber();

            var citySpread = new FakeCitySpread(disease, new List<DiseaseStatistic>(), peopleNumber);
            var Re = citySpread.CalculateRe();

            Assert.GreaterOrEqual(Re, disease.R0Min);
            Assert.LessOrEqual(Re, disease.R0Max);
        }

        [Test]
        public void CalculateRe_AllPeopleWereInjured_ReturnsZero()
        {
            var disease = _parameterSetter.CreateDisease();
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var diseaseStatistic = new DiseaseStatistic { Injured = peopleNumber };

            var citySpread = new FakeCitySpread(disease, new List<DiseaseStatistic> { diseaseStatistic }, peopleNumber);
            var Re = citySpread.CalculateRe();

            Assert.Zero(Re);
        }

        [Test]
        public void CalculateRe_AllPeopleWereRecovered_ReturnsZero()
        {
            var disease = _parameterSetter.CreateDisease();
            disease.AcquireImmunity = true;
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var diseaseStatistic = new DiseaseStatistic { Recovered = peopleNumber };

            var citySpread = new FakeCitySpread(disease, new List<DiseaseStatistic> { diseaseStatistic }, peopleNumber);
            var Re = citySpread.CalculateRe();

            Assert.Zero(Re);
        }

        [Test]
        public void GetCanBecomeSick_YesterdayStatisticIsNull_ReturnCityPeopleNumber()
        {
            var disease = _parameterSetter.CreateDisease();
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var emptyDiseaseStatistics = new List<DiseaseStatistic> { };

            var citySpread = new FakeCitySpread(disease, emptyDiseaseStatistics, peopleNumber);
            var canBecomeSick = citySpread.GetCanBecomeSick();

            Assert.AreEqual(canBecomeSick, peopleNumber);
        }

        [Test]
        public void GetCanBecomeSick_DiseaseHasAcquireImmunity_ReturnCityPeopleNumberMinusAllStatistics()
        {
            var disease = _parameterSetter.CreateDisease();
            disease.AcquireImmunity = true;
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var diseaseStatistic = new DiseaseStatistic 
            { 
                Recovered = (peopleNumber / 5),
                Injured = (peopleNumber / 5),
                Sick = (peopleNumber / 5),
                Dead = (peopleNumber / 5)
            };

            var citySpread = new FakeCitySpread(disease, new List<DiseaseStatistic> { diseaseStatistic }, peopleNumber);
            var canBecomeSick = citySpread.GetCanBecomeSick();

            var expectedCanBecomeSick = peopleNumber - diseaseStatistic.Dead - diseaseStatistic.Injured - 
                                                                            diseaseStatistic.Recovered - diseaseStatistic.Sick;
            Assert.AreEqual(canBecomeSick, expectedCanBecomeSick);
        }

        [Test]
        public void GetSickInDurationDay_DiseaseStatisticIsLessThanDiseaseDuration_ReturnZero()
        {
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var diseaseStatistics = new List<DiseaseStatistic> { new DiseaseStatistic() };
            var disease = _parameterSetter.CreateDisease();
            disease.Duration = diseaseStatistics.Count + 1;

            var citySpread = new FakeCitySpread(disease, diseaseStatistics, peopleNumber);
            var sickInDurationDay = citySpread.GetSickInDurationDay();

            Assert.Zero(sickInDurationDay);
        }

        [Test]
        public void GetSickInDurationDay_DiseaseStatisticIsEqualDiseaseDuration_ReturnDurationStartDaySick()
        {
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var durationStartDayStatistic = new DiseaseStatistic { Sick = peopleNumber };
            var diseaseStatistics = new List<DiseaseStatistic> { durationStartDayStatistic, new DiseaseStatistic () };
            var disease = _parameterSetter.CreateDisease();
            disease.Duration = diseaseStatistics.Count;

            var citySpread = new FakeCitySpread(disease, diseaseStatistics, peopleNumber);
            var sickInDurationDay = citySpread.GetSickInDurationDay();

            Assert.AreEqual(sickInDurationDay, durationStartDayStatistic.Sick);
        }

        [Test]
        public void GetSickInDurationDay_DiseaseStatisticIsMoreThanDiseaseDuration_ReturnDurationStartDaySickMinusPreviousDaySick()
        {
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var previousDurationStartDayStatistic = new DiseaseStatistic { Sick = peopleNumber / 2 };
            var durationStartDayStatistic = new DiseaseStatistic { Sick = peopleNumber / 2 };
            var diseaseStatistics = new List<DiseaseStatistic> 
            {   previousDurationStartDayStatistic, 
                durationStartDayStatistic, 
                new DiseaseStatistic() 
            };
            var disease = _parameterSetter.CreateDisease();
            disease.Duration = diseaseStatistics.Count - 1;

            var citySpread = new FakeCitySpread(disease, diseaseStatistics, peopleNumber);
            var sickInDurationDay = citySpread.GetSickInDurationDay();

            var expectedSickInDurationDay = durationStartDayStatistic.Sick - previousDurationStartDayStatistic.Sick;
            Assert.AreEqual(sickInDurationDay, expectedSickInDurationDay);
        }

   
        [Test]
        public void GetDeadBeforeToday_FirstDayOfEpidemy_ReturnZero()
        {
            var disease = _parameterSetter.CreateDisease();
            var peopleNumber = _parameterSetter.GetPeopleNumber();

            var citySpread = new FakeCitySpread(disease, new List<DiseaseStatistic> {  }, peopleNumber);
            var deadBeforeToday = citySpread.GetDeadBeforeToday();

            Assert.Zero(deadBeforeToday);
        }

        [Test]
        public void GetDeadBeforeToday_NotFirstDayOfEpidemy_ReturnYesterdayDead()
        {
            var disease = _parameterSetter.CreateDisease();
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var diseaseStatistic = new DiseaseStatistic { Dead = peopleNumber };

            var citySpread = new FakeCitySpread(disease, new List<DiseaseStatistic> { diseaseStatistic  }, peopleNumber);
            var deadBeforeToday = citySpread.GetDeadBeforeToday();

            Assert.AreEqual(deadBeforeToday, diseaseStatistic.Dead);
        }

        [Test]
        public void CheckForOverload_CheckMoreThanCityPeopleNumber_ReturnCityPeopleNumber()
        {
            var disease = _parameterSetter.CreateDisease();
            var peopleNumber = _parameterSetter.GetPeopleNumber();
            var checking = peopleNumber + 1;

            var citySpread = new FakeCitySpread(disease, new List<DiseaseStatistic> { }, peopleNumber);
            var checkForOverload = citySpread.CheckForOverload(checking);

            Assert.AreEqual(peopleNumber, checkForOverload);
        }
    }
}
