using DiseaseMapMVC.Models.Disease;
using DiseaseMongoModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace DiseaseMapMVC.UnitTests
{
    [TestFixture]
    public class DiseaseSpreadFactoryTests
    {
        [Test]
        public void Create_EmptyMKBCode_ThrowException()
        {
            var disease = new Disease();

            var factory = new DiseaseSpreadFactory();
            var diseaseStatistics = new List<DiseaseStatistic>();

            Assert.Catch<ArgumentNullException>(() => factory.Create(disease, diseaseStatistics, default(int)));
        }

        [Test]
        [TestCase("B05", typeof(MeaslesCitySpread))]
        [TestCase("U07.1", typeof(CoronavirusCitySpread))]
        public void Create_SetMKBCode_ReturnsCorrectType(string mkbCode, Type type)
        {
            var disease = new Disease { MKBCode = mkbCode };

            var factory = new DiseaseSpreadFactory();
            var diseaseStatistics = new List<DiseaseStatistic>();

            var diseaseSpread = factory.Create(disease, diseaseStatistics, default(int));

            Assert.True(diseaseSpread.GetType() == type);
        }

        [Test]
        public void Create_SetUnExistedMKBCode_ThrowException()
        {
            var disease = new Disease { MKBCode = "NoNumber" };

            var factory = new DiseaseSpreadFactory();
            var diseaseStatistics = new List<DiseaseStatistic>();

            Assert.Catch<ArgumentOutOfRangeException>(() => factory.Create(disease, diseaseStatistics, default(int)));
        }
    }
}
