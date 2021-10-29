using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Город
    /// </summary>
    public class City : BaseEntity
    {
        /// <summary>
        /// Название города
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id страны
        /// </summary>
        public ObjectId CountryId { get; set; }

        /// <summary>
        /// Население города
        /// </summary>
        public CityPopulation Population { get; set; }
    }
}
