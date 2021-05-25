using DiseaseMapMVC.Models.Disease;
using DiseaseMongoModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace DiseaseMapMVC.UnitTests
{
    [TestFixture]
    public class DiseaseSpreadCreatorTests
    {
        [Test]
        public void CreateForCity_EmptyMKBCode_ThrowException()
        {
            var disease = new Disease();

            var diseaseSpreadCreator = new DiseaseSpreadCreator();
            var diseaseStatistics = new List<DiseaseStatistic>();

            Assert.Catch<ArgumentNullException>(() => diseaseSpreadCreator.CreateForCity(disease, diseaseStatistics, default(int)));
        }

        [Test]
        [TestCase("B05", typeof(MeaslesCitySpread))]
        [TestCase("U07.1", typeof(CoronavirusCitySpread))]
        public void CreateForCity_SetMKBCode_ReturnsCorrectType(string mkbCode, Type type)
        {
            var disease = new Disease { MKBCode = mkbCode };

            var diseaseSpreadCreator = new DiseaseSpreadCreator();
            var diseaseStatistics = new List<DiseaseStatistic>();

            var diseaseSpread = diseaseSpreadCreator.CreateForCity(disease, diseaseStatistics, default(int));

            Assert.True(diseaseSpread.GetType() == type);
        }

        [Test]
        public void CreateForCity_SetUnExistedMKBCode_ThrowException()
        {
            var disease = new Disease { MKBCode = "NoNumber" };

            var diseaseSpreadCreator = new DiseaseSpreadCreator();
            var diseaseStatistics = new List<DiseaseStatistic>();

            Assert.Catch<ArgumentOutOfRangeException>(() => diseaseSpreadCreator.CreateForCity(disease, diseaseStatistics, default(int)));
        }
    }
}
