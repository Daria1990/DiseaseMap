using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public ObjectId CountryId { get; set; }

        public CityPopulation Population { get; set; }
    }
}
