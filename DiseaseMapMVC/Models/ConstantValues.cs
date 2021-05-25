using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models
{
    public static class ConstantValues
    {
        /// <summary>
        /// сколько минут в кэше хранятся данные
        /// </summary>
        public const int MINUTES_IN_CACHE = 5;

        public static class SessionKey
        {
            public const string COUNTRY_EPIDEMIC_ID_KEY = "countryEpidemicId";
        }

        public static class Disease
        {
            /// <summary>
            /// значение по умолчанию дней, за которые рассчитывается коэффициент распространения у коронавируса 
            /// </summary>
            public const int DEFAULT_CORONAVIRUS_DAYS_SPREAD_COUNT = 8;
        }
    }
}
