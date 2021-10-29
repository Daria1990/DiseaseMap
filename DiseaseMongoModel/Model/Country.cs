using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Страна
    /// </summary>
    public class Country : BaseEntity
    {
        /// <summary>
        /// Название страны
        /// </summary>
        public string Name { get; set; }
    }
}
