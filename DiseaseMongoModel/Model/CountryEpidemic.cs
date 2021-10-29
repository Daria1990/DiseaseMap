using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Эпидемия в стране
    /// </summary>
    public class CountryEpidemic : BaseEntity
    {
        /// <summary>
        /// Количество дней от начала эпидемии
        /// </summary>
        public int DayFromStart { get; set; }

        /// <summary>
        /// Id заболевания
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string DiseaseId { get; set; }

        /// <summary>
        /// Id страны, в которой происходит эпидемия
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string CountryId { get; set; }

        /// <summary>
        /// Ситуация с эпидемией по городам страны
        /// </summary>
        public List<CityEpidemic> CityEpidemics { get; set; }
    }
}
