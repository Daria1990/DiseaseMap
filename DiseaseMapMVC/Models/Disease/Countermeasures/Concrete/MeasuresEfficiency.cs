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
    public class MeasuresEfficiency : IMeasuresEfficiency
    {
        /// <summary>
        /// Получить процент людей, подвергающихся риску заболеть, с учетом принятых контрмер
        /// </summary>
        /// <param name="countermeasures">Контрмеры</param>
        /// <param name="cityPopulation">население города</param>
        /// <returns>процент людей, которые могут заразиться</returns>
        public double GetPeopleInRickProcent(Countermeasures countermeasures, CityPopulation cityPopulation)
        {
            if (cityPopulation == null || cityPopulation.PeopleNumber == 0)
            {
                return 1;
            }

            var peopleInRisk = cityPopulation.PeopleNumber;

            if (countermeasures.CloseKinderGartens)
            {
                peopleInRisk -= cityPopulation.KidNumber;
            }

            if (countermeasures.RemoteWork)
            {
                peopleInRisk -= cityPopulation.AdultNumber;
            }

            if (countermeasures.CloseSchools)
            {
                peopleInRisk -= cityPopulation.AdolescentNumber;
            }

            if (countermeasures.StopTransport)
            {
                peopleInRisk -= cityPopulation.RetireeNumber;
            }

            return (double)peopleInRisk / cityPopulation.PeopleNumber;
        }
    }
}
