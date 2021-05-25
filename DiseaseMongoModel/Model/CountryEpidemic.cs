using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DiseaseMongoModel
{
    public class CountryEpidemic : BaseEntity
    {
        public int DayFromStart { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string DiseaseId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CountryId { get; set; }

        public List<CityEpidemic> CityEpidemics { get; set; }

        public static CountryEpidemic SetCityEpidemicParameters(CountryEpidemic countryEpidemic)
        {
            throw new NotImplementedException();
        }
    }
}
