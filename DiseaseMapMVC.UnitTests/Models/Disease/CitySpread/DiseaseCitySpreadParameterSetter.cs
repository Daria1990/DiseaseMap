using System;
using DiseaseMongoModel;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    public class DiseaseCitySpreadParameterSetter
    {
        private readonly int MAX_R0 = 20;
        private readonly int MAX_PEOPLE = 1000;

        public Disease CreateDisease()
        {
            var random = new Random();

            var R0Min = random.Next(0, MAX_R0);
            var R0Max = random.Next(R0Min, MAX_R0);

            return new Disease { R0Max = R0Max, R0Min = R0Min };
        }

        public CityPopulation CreateCityPopulation()
        {
            var random = new Random();

            return new CityPopulation
            {
                AdolescentNumber = random.Next(MAX_PEOPLE),
                AdultNumber = random.Next(MAX_PEOPLE),
                KidNumber = random.Next(MAX_PEOPLE),
                RetireeNumber = random.Next(MAX_PEOPLE)
            };
        }

        public int GetPeopleNumber()
        {
            var random = new Random();
            return random.Next(0, MAX_PEOPLE);
        }
    }
}
