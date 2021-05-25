using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    public class CityEpidemic : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string CityId { get; set; }

        public string CityName { get; set; }

        public List<DiseaseStatistic> DiseaseStatistics { get; set; }

        public Countermeasures Countermeasures { get; set; }

        public int VaccinesDoneDayily { get; set; }
    }
}
