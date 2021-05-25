using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    public class DiseaseStatistic
    {
        /// <summary>
        /// Заболевшие
        /// </summary>
        public int Sick { get; set; }

        /// <summary>
        /// Выздоровевшие
        /// </summary>
        public int Recovered { get; set; }

        /// <summary>
        /// Умершие
        /// </summary>
        public int Dead { get; set; } 

        /// <summary>
        /// Привитые
        /// </summary>
        public int Injured { get; set; }
    }
}
