using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Эпидемия в городе
    /// </summary>
    public class CityEpidemic : BaseEntity
    {
        /// <summary>
        /// Id города, в котором происходит эпидемия
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string CityId { get; set; }

        /// <summary>
        /// Наименование города
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Статистика заболеваемости по дням
        /// </summary>
        public List<DiseaseStatistic> DiseaseStatistics { get; set; }

        /// <summary>
        /// Меры, предпринимаемые администрацией города для предотвращения распространения заболевания
        /// </summary>
        public Countermeasures Countermeasures { get; set; }

        /// <summary>
        /// Количество людей, которые могут быть вакцинированы за сутки
        /// </summary>
        public int VaccinesDoneDayily { get; set; }
    }
}
