using NUnit.Framework;
using DiseaseMapMVC.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    [TestFixture]
    public class RandomExtensionsTests
    {
        [Test]
        public void NextDouble_ZeroParameters_ReturnsZero()
        {
            var random = new Random();
            var randomResult = random.NextDouble(0, 0);

            Assert.Zero(randomResult);
        }

        [Test]
        [TestCase(10, 10, ExpectedResult = 10)]
        [TestCase(15, 15, ExpectedResult = 15)]
        public double NextDouble_EqualParameters_ReturnsMinValue(double minValue, double maxValue)
        {
            var random = new Random();
            return random.NextDouble(minValue, maxValue);
        }

        [Test]
        [TestCase(9, 9)]
        [TestCase(5, 5)]
        public void NextDouble_MaxValueMoreThanMinValue_ReturnsPositiveNumber(double minValue, double maxValue)
        {
            var random = new Random();
            var randomResult = random.NextDouble(minValue, maxValue);

            Assert.True(randomResult > 0);
        }
    }
}
