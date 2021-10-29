//using DiseaseMapMVC.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Болезнь
    /// </summary>
    public class Disease : BaseEntity
    {
        public string Name { get; set; }

        /// <summary>
        /// Код болезни по МКБ
        /// </summary>
        public string MKBCode { get; set; }

        /// <summary>
        /// Вакцины
        /// </summary>
        public List<Vaccine> Vaccines { get; set; }

        /// <summary>
        /// Сколько один больной заражает человек (max)
        /// </summary>
        public double R0Max { get; set; }

        /// <summary>
        /// Сколько один больной заражает человек (min)
        /// </summary>
        public double R0Min { get; set; }
        
        /// <summary>
        /// Индекс смертности в процентах
        /// </summary>
        public double DeathIndex { get; set; }

        /// <summary>
        /// Появляется ли иммунитет у переболевших
        /// </summary>
        public bool AcquireImmunity { get; set; }

        /// <summary>
        /// Продолжительность болезни
        /// </summary>
        public int Duration { get; set; }
    }
}
