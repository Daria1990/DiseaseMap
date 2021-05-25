using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    /// <summary>
    /// Класс для рассчета эффективности мер, принимаемых для борьбы с инфекцией
    /// </summary>
    public class MeasuresEfficiency
    {
        private Countermeasures _countermeasures;
        private CityPopulation _cityPopulation;

        public MeasuresEfficiency(Countermeasures countermeasures, CityPopulation cityPopulation)
        {
            _countermeasures = countermeasures;
            _cityPopulation = cityPopulation;
        }

        public double GetPeopleInRickProcent()
        {
            if (_cityPopulation == null || _cityPopulation.PeopleNumber == 0)
            {
                return 1;
            }

            var peopleInRisk = _cityPopulation.PeopleNumber;

            if (_countermeasures.CloseKinderGartens)
            {
                peopleInRisk -= _cityPopulation.KidNumber;
            }

            if (_countermeasures.RemoteWork)
            {
                peopleInRisk -= _cityPopulation.AdultNumber;
            }

            if (_countermeasures.CloseSchools)
            {
                peopleInRisk -= _cityPopulation.AdolescentNumber;
            }

            if (_countermeasures.StopTransport)
            {
                peopleInRisk -= _cityPopulation.RetireeNumber;
            }

            return (double)peopleInRisk / _cityPopulation.PeopleNumber;
        }
    }
}
