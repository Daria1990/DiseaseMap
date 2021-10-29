using System;
using DiseaseMongoModel;
using System.Collections.Generic;
using System.Text;

namespace DiseaseMapMVC.UnitTests
{
    public class DiseaseCitySpreadParameterSetter
    {
        private readonly int _MaxR0 = 20;
        private readonly int _MaxPeople = 1000;

        public Disease CreateDisease()
        {
            var random = new Random();

            var R0Min = random.Next(0, _MaxR0);
            var R0Max = random.Next(R0Min, _MaxR0);

            return new Disease { R0Max = R0Max, R0Min = R0Min };
        }

        public CityPopulation CreateCityPopulation()
        {
            var random = new Random();

            return new CityPopulation
            {
                AdolescentNumber = random.Next(_MaxPeople),
                AdultNumber = random.Next(_MaxPeople),
                KidNumber = random.Next(_MaxPeople),
                RetireeNumber = random.Next(_MaxPeople)
            };
        }

        public int GetPeopleNumber()
        {
            var random = new Random();
            return random.Next(0, _MaxPeople);
        }
    }
}
