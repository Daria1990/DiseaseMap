using DiseaseMongoModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiseaseDataFilling.DataFilling
{
    /// <summary>
    /// Класс заполнения базы данных Mongo данными
    /// </summary>
    public class MongoDataFilling
    {
        /// <summary>
        /// Метод заполняет заданную базу данных данными
        /// </summary>
        /// <param name="connectionString">строка подключения к базе данных</param>
        public void FillDiseaseDb(string connectionString)
        {
            var mongoContext = new MongoContext(connectionString);

            CreateCountries(mongoContext);
            CreateDiseasies(mongoContext);
        }

        /// <summary>
        /// Метод создает страны с городами в базе данных Mongo
        /// </summary>
        /// <param name="mongoContext">контекст подключения базы данных Mongo</param>
        private void CreateCountries(MongoContext mongoContext)
        {
            var repositoryCountry = new MongoDbRepository<Country>(mongoContext);

            var repositoryCity = new MongoDbRepository<City>(mongoContext);

            #region Россия

            var russia = new Country { Name = "Russia" };

            if (repositoryCountry.Create(russia))
                Console.WriteLine("Country Russia was added");

            var russianId = new ObjectId(russia.Id);

            #region города России

            var moscowPopulation = new CityPopulation { AdolescentNumber = 300000, AdultNumber = 300000, 
                                        KidNumber = 300000, RetireeNumber = 300000 };

            var moscowCity = new City { Name = "Moscow", Population = moscowPopulation, CountryId = russianId };

            if (repositoryCity.Create(moscowCity))
                Console.WriteLine("City Moscow was added");

            var stpetersburgPopulation = new CityPopulation { AdolescentNumber = 100000, AdultNumber = 100000,
                                            KidNumber = 100000, RetireeNumber = 100000 };

            var stpetersburgCity = new City { Name = "St. Petersburg", Population = stpetersburgPopulation, CountryId = russianId };

            if (repositoryCity.Create(stpetersburgCity))
                Console.WriteLine("City St.Petersburg was added");

            var novosibirskCityPopulation = new CityPopulation { AdolescentNumber = 300000, AdultNumber = 600000,
                KidNumber = 300000, RetireeNumber = 440000 };

            var novosibirskCity = new City { Name = "Novosibirsk", Population = novosibirskCityPopulation, CountryId = russianId };

            if (repositoryCity.Create(novosibirskCity))
                Console.WriteLine("City Novosibirsk was added");

            var tomskPopulation = new CityPopulation { AdolescentNumber = 150000, AdultNumber = 300000,
                KidNumber = 70000, RetireeNumber = 220000 };

            var tomskCity = new City { Name = "Tomsk", Population = tomskPopulation, CountryId = russianId };

            if (repositoryCity.Create(tomskCity))
                Console.WriteLine("City Tomsk was added");

            var omskPopulation = new CityPopulation { AdolescentNumber = 160000, AdultNumber = 600000,
                KidNumber = 60000, RetireeNumber = 300000};

            var omskCity = new City { Name = "Omsk", Population = omskPopulation, CountryId = russianId };

            if (repositoryCity.Create(omskCity))
                Console.WriteLine("City Omsk was added");

            var ekaterinburgPopulation = new CityPopulation { AdolescentNumber = 160000, AdultNumber = 600000,
                KidNumber = 60000, RetireeNumber = 300000 };

            var ekaterinburgCity = new City { Name = "Ekaterinburg", Population = ekaterinburgPopulation, CountryId = russianId };

            if (repositoryCity.Create(ekaterinburgCity))
                Console.WriteLine("City Ekaterinburg was added");

            var kostromaPopulation = new CityPopulation { AdolescentNumber = 50000, AdultNumber = 120000,
                KidNumber = 30000, RetireeNumber = 50000 };

            var kostromaCity = new City { Name = "Kostroma", Population = kostromaPopulation, CountryId = russianId };

            if (repositoryCity.Create(kostromaCity))
                Console.WriteLine("City Kostroma was added");

            var irkutskPopulation = new CityPopulation { AdolescentNumber = 50000, AdultNumber = 300000,
                KidNumber = 50000, RetireeNumber = 200000 };

            var irkutskCity = new City { Name = "Irkutsk", Population = irkutskPopulation, CountryId = russianId };

            if (repositoryCity.Create(irkutskCity))
                Console.WriteLine("City Irkutsk was added");

            var tverePopulation = new CityPopulation { AdolescentNumber = 50000, AdultNumber = 200000,
                KidNumber = 50000, RetireeNumber = 100000 };

            var tvereCity = new City { Name = "Tvere", Population = tverePopulation, CountryId = russianId };

            if (repositoryCity.Create(tvereCity))
                Console.WriteLine("City Tvere was added");

            var kaliningradPopulation = new CityPopulation { AdolescentNumber = 50000, AdultNumber = 200000,
                KidNumber = 70000, RetireeNumber = 100000 };

            var kaliningradCity = new City { Name = "Kaliningrad", Population = kaliningradPopulation, CountryId = russianId };

            if (repositoryCity.Create(kaliningradCity))
                Console.WriteLine("City Kaliningrad was added");

            var vladivostokPopulation = new CityPopulation { AdolescentNumber = 50000, AdultNumber = 400000,
                KidNumber = 100000, RetireeNumber = 100000 };

            var vladivostokCity = new City { Name = "Vladivostok", Population = vladivostokPopulation, CountryId = russianId };

            if (repositoryCity.Create(vladivostokCity))
                Console.WriteLine("City Vladivostok was added");

            #endregion города России

            #endregion Россия

            #region США

            var usa = new Country { Name = "USA" };

            if (repositoryCountry.Create(usa))
                Console.WriteLine("Country USA was added");

            var usaId = new ObjectId(usa.Id);

            #region города США

            var nyPopulation = new CityPopulation { AdolescentNumber = 300000, AdultNumber = 300000,
                KidNumber = 300000, RetireeNumber = 300000 };

            var nyCity = new City { Name = "New York", Population = nyPopulation, CountryId = usaId };

            if (repositoryCity.Create(nyCity))
                Console.WriteLine("City New York was added");

            #endregion города США
            #endregion США
        }

        /// <summary>
        /// Метод создает болезни и связанные с ними данные в базе данных Mongo
        /// </summary>
        /// <param name="mongoContext">контекст подключения базы данных Mongo</param>
        private void CreateDiseasies(MongoContext mongoContext)
        {
            var repository = new MongoDbRepository<Disease>(mongoContext);

            #region Корь
            var mmr = new Vaccine { Name = "MMR", ActionDay = 3};

            var measles = new Disease { Name = "Measles", R0Max = 18, MKBCode = "B05", Duration = 28,
                R0Min = 12, DeathIndex = 0.1, AcquireImmunity = true, Vaccines = new List<Vaccine> { mmr } };

            if (repository.Create(measles))
                Console.WriteLine("Disease measles was added");
            #endregion Корь

            #region Коронавирус
            var sputnik = new Vaccine { Name = "Sputnik-V", ActionDay = 21 };

            var coronavirus = new Disease { Name = "Coronavirus", R0Max = 2, MKBCode = "U07.1", Duration = 14, 
                R0Min = 0.5, DeathIndex = 0.01, AcquireImmunity = true, Vaccines = new List<Vaccine> { sputnik },
            };

            if (repository.Create(coronavirus))
                Console.WriteLine("Disease coronavirus was added");
            #endregion Коронавирус
        }
    }
}
