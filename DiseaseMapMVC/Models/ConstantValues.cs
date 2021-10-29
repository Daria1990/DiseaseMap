using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models
{
    /// <summary>
    /// Класс для хранения неизменяемых значений
    /// </summary>
    public class ConstantValues
    {
        /// <summary>
        /// сколько минут в кэше хранятся данные
        /// </summary>
        public const int MinutesInCache = 5;

        /// <summary>
        /// Ключ сессии для Id эпидемии в стране
        /// </summary>
        public const string CountryEpidemicSessionKey = "countryEpidemicId";

        /// <summary>
        /// значение по умолчанию дней, за которые рассчитывается коэффициент распространения у коронавируса 
        /// </summary>
        public const int DefaultCoronavirusDaysSpreadCount = 8;
    }
}
