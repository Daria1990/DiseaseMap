using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Disease
{
    /// <summary>
    /// Компания по вакцинации
    /// </summary>
    public interface IVaccinationCompany
    {
        /// <summary>
        /// Количество вакцинированных за сегодня людей
        /// </summary>
        /// <param name="vaccinesDoneDayily">Количество вакцин, которые делают за один день</param>
        /// <param name="needVaccination">Количество людей, которым все еще необходима вакцинация</param>
        /// <returns>вакцинированные за сегодня люди</returns>
        public int GetTodaysVaccianted(int vaccinesDoneDayily, int needVaccination);
    }
}
