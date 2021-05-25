using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    /// <summary>
    /// Класс населения города
    /// </summary>
    public class CityPopulation 
    {
        public int KidNumber { get; set; }

        public int AdolescentNumber { get; set; }

        public int AdultNumber { get; set; }

        public int RetireeNumber { get; set; }

        [BsonIgnore]
        public int PeopleNumber 
        { 
            get 
            { 
                return KidNumber + AdolescentNumber + AdultNumber + RetireeNumber; 
            } 
        }
    }
}
