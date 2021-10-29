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
        /// <summary>
        /// Количество детей
        /// </summary>
        public int KidNumber { get; set; }

        /// <summary>
        /// Количество подростков
        /// </summary>
        public int AdolescentNumber { get; set; }

        /// <summary>
        /// Количество работоспособных взрослых
        /// </summary>
        public int AdultNumber { get; set; }

        /// <summary>
        /// Количество пенсионеров
        /// </summary>
        public int RetireeNumber { get; set; }

        /// <summary>
        /// Общее количество жителей города
        /// </summary>
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
