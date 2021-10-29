using DiseaseMongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    /// <summary>
    /// Интерфейс для рассчета эффективности мер, принимаемых для борьбы с инфекцией
    /// </summary>
    public interface IMeasuresEfficiency
    {
        /// <summary>
        /// Получить процент людей, подвергающихся риску заболеть, с учетом принятых контрмер
        /// </summary>
        /// <param name="countermeasures">Контрмеры</param>
        /// <param name="cityPopulation">население города</param>
        /// <returns>процент людей, которые могут заразиться</returns>
        double GetPeopleInRickProcent(Countermeasures countermeasures, CityPopulation cityPopulation);
    }
}
