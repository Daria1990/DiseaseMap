using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    public class DiseaseStatisticCreator
    { 
        public DiseaseStatistic CreateForToday(DiseaseCitySpread diseaseCitySpread, CityEpidemic cityEpidemic, City city)
        {
            var dead = diseaseCitySpread.CalculateDead();

            var measuresEfficiency = new MeasuresEfficiency(cityEpidemic.Countermeasures, city.Population);
            var peopleInRiskProcent = measuresEfficiency.GetPeopleInRickProcent();
            var sick = diseaseCitySpread.CalculateSick(peopleInRiskProcent);

            var recovered = diseaseCitySpread.CalculateRecovered();

            var vaccinationCompany = new VaccinationCompany(cityEpidemic.VaccinesDoneDayily);
            var injured = diseaseCitySpread.CalculateInjured(vaccinationCompany);

            return new DiseaseStatistic { Dead = dead, Sick = sick, Recovered = recovered, Injured = injured };
        }
    }
}
