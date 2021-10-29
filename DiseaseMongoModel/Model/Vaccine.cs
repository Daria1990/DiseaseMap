using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Класс прививки
    /// </summary>
    public class Vaccine
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// День действие прививки
        /// </summary>
        public int ActionDay { get; set; }

        /// <summary>
        /// Процент эффективности прививки
        /// </summary>
        public double EfficiencyPercent { get; set; }
    }
}
