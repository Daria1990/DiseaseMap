using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiseaseMapMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DiseaseMongoModel;
using MongoDB.Bson;
using DiseaseMapMVC.Models.Disease;
using DiseaseMapMVC.Models.Repositories;
using Microsoft.Extensions.Caching.Memory;
using DiseaseMapMVC.Models;
using Microsoft.AspNetCore.Http;

namespace DiseaseMapMVC.Controllers
{
    public class EpidemicSpreadController : Controller
    {
        private CacheRepository<City> _cityRepository;

        private CacheRepository<Disease> _diseaseRepository;

        private CacheRepository<CountryEpidemic> _countryEpidemicRepository;

        public EpidemicSpreadController(IMemoryCache memoryCache, MongoContext context)
        {
            var memoryCacheWrapper = new MemoryCacheWrapper(memoryCache);

            _cityRepository = new CacheRepository<City>(memoryCacheWrapper, new MongoDbRepository<City>(context));

            _diseaseRepository = new CacheRepository<Disease>(memoryCacheWrapper, new MongoDbRepository<Disease>(context));

            _countryEpidemicRepository = new CacheRepository<CountryEpidemic>(memoryCacheWrapper, new MongoDbRepository<CountryEpidemic>(context));
        }

        [HttpGet]
        public IActionResult Index(string countryEpidemicId)
        {
            if (string.IsNullOrEmpty(countryEpidemicId))
            {
                countryEpidemicId = GetDataFromHttpContext(ConstantValues.SessionKey.COUNTRY_EPIDEMIC_ID_KEY);
            }

            var countryEpidemic = _countryEpidemicRepository.GetById(countryEpidemicId);

            return View(countryEpidemic);
        }

        private string GetDataFromHttpContext(string key)
        {
            var value = HttpContext.Session.GetString(key);

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(key);
            }
            else
            {
                return value;
            }
        }

        [HttpPost]
        public IActionResult Index(CountryEpidemic countryEpidemic)
        {
            _countryEpidemicRepository.Update(countryEpidemic);

            return View("DistributionStatistics", countryEpidemic);
        }


        [HttpGet]
        public IActionResult GoToPreviousStep(string countryEpidemicId, int currentDay)
        {
            var countryEpidemic = _countryEpidemicRepository.GetById(countryEpidemicId);

            if (currentDay > 1)
            {
                DeleteLastStatistic(countryEpidemic);

                countryEpidemic.DayFromStart = currentDay - 1;

                return View("DistributionStatistics", countryEpidemic);
            }
            else
            {
                var parameterSetter = new CountryEidemicParameterSetter(countryEpidemic);
                //рассчитываем все заново
                countryEpidemic = parameterSetter.SetCities();

                _countryEpidemicRepository.Update(countryEpidemic);

                return View("Index", countryEpidemic);
            }
        }

        
        private void DeleteLastStatistic(CountryEpidemic countryEpidemic)
        {
            var lastStatisticNumber = countryEpidemic.DayFromStart - 1;

            for (int i = 0; i < countryEpidemic.CityEpidemics.Count; i++)
            {
                countryEpidemic.CityEpidemics[i].DiseaseStatistics.RemoveAt(lastStatisticNumber);
            }
        }

        [HttpGet]
        public IActionResult GoToNextStep(string countryEpidemicId, int currentDay)
        {
            var countryEpidemic = _countryEpidemicRepository.GetById(countryEpidemicId);

            if (currentDay == countryEpidemic.DayFromStart)
            {
                GetCurrentCountryEpidemic(countryEpidemic);
                _countryEpidemicRepository.Update(countryEpidemic);
            }
            else
            {
                countryEpidemic.DayFromStart = currentDay + 1;
            }

            return View("DistributionStatistics", countryEpidemic);
        }

        private void GetCurrentCountryEpidemic(CountryEpidemic countryEpidemic)
        {
            var disease = _diseaseRepository.GetById(countryEpidemic.DiseaseId);

            foreach (var cityEpidemic in countryEpidemic.CityEpidemics)
            {
                var todaysStatistic = GetTodayCityEpidemicStatistic(cityEpidemic, disease);
                cityEpidemic.DiseaseStatistics.Add(todaysStatistic);
            }

            countryEpidemic.DayFromStart += 1;
        }

        private DiseaseStatistic GetTodayCityEpidemicStatistic(CityEpidemic cityEpidemic, Disease disease)
        {
            var city = _cityRepository.GetById(cityEpidemic.CityId.ToString());

            var diseaseCitySpread = GetDiseaseSpread(disease, cityEpidemic.DiseaseStatistics, city.Population.PeopleNumber);

            var diseaseStatisticCreator = new DiseaseStatisticCreator();

            return diseaseStatisticCreator.CreateForToday(diseaseCitySpread, cityEpidemic, city);
        }

        private DiseaseCitySpread GetDiseaseSpread(Disease disease, List<DiseaseStatistic> diseaseStatistics, int cityPeopleNumber)
        {
            var diseaseSpreadCreator = new DiseaseSpreadCreator();

            return diseaseSpreadCreator.CreateForCity(disease, diseaseStatistics, cityPeopleNumber);
        }
    }
}