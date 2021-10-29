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
using DiseaseMapMVC.Models.Cache;
using Microsoft.Extensions.Caching.Memory;
using DiseaseMapMVC.Models;
using Microsoft.AspNetCore.Http;
using DiseaseMapMVC.Models.Epidemic;

namespace DiseaseMapMVC.Controllers
{
    /// <summary>
    /// Контроллер рассчета распространения заболевания
    /// </summary>
    public class EpidemicSpreadController : Controller
    {
        /// <summary>
        /// Фабрика создания классов для рассчета распространения заболевания
        /// </summary>
        private IDiseaseSpreadFactory _DiseaseSpreadFactory;

        /// <summary>
        /// Рассчет статистики заболеваемости
        /// </summary>
        private IDiseaseStatisticCalculator _DiseaseStatisticCalculator;

        /// <summary>
        /// Устанановка значения полей для эпидемии
        /// </summary>
        private ICountryEpidemicParameterSetter _CountryEpidemicParameterSetter;

        /// <summary>
        /// Репозиторий городов
        /// </summary>
        private CacheRepository<City> _CityCacheRepository;

        /// <summary>
        /// Репоиторий болезней
        /// </summary>
        private CacheRepository<Disease> _DiseaseCacheRepository;

        /// <summary>
        /// Репозиторий стран
        /// </summary>
        private CacheRepository<CountryEpidemic> _CountryEpidemicCacheRepository;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="memoryCacheWrapper">обертка для кеширования на сервере</param>
        /// <param name="context"></param>
        /// <param name="diseaseSpreadFactory">Фабрика создания классов для рассчета распространения заболевания</param>
        /// <param name="diseaseStatisticCalculator">Рассчет статистики заболеваемости</param>
        /// <param name="countryEpidemicParameterSetter">Устанановка значения полей для эпидемии</param>
        public EpidemicSpreadController(IMemoryCacheWrapper memoryCacheWrapper, MongoContext context, IDiseaseSpreadFactory diseaseSpreadFactory,
                                IDiseaseStatisticCalculator diseaseStatisticCalculator, ICountryEpidemicParameterSetter countryEpidemicParameterSetter)
        {
            if (memoryCacheWrapper == null)
            {
                throw new ArgumentNullException(nameof(memoryCacheWrapper));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _CityCacheRepository = new CacheRepository<City>(memoryCacheWrapper, new MongoDbRepository<City>(context));
            _DiseaseCacheRepository = new CacheRepository<Disease>(memoryCacheWrapper, new MongoDbRepository<Disease>(context));
            _CountryEpidemicCacheRepository = new CacheRepository<CountryEpidemic>(memoryCacheWrapper, new MongoDbRepository<CountryEpidemic>(context));

            _DiseaseSpreadFactory = diseaseSpreadFactory ?? throw new ArgumentNullException(nameof(diseaseSpreadFactory));
            _DiseaseStatisticCalculator = diseaseStatisticCalculator ?? throw new ArgumentNullException(nameof(diseaseStatisticCalculator));
            _CountryEpidemicParameterSetter = countryEpidemicParameterSetter ?? throw new ArgumentNullException(nameof(countryEpidemicParameterSetter));
        }

        [HttpGet]
        public IActionResult Index(string countryEpidemicId)
        {
            if (string.IsNullOrEmpty(countryEpidemicId))
            {
                countryEpidemicId = GetDataFromHttpContext(ConstantValues.CountryEpidemicSessionKey);
            }

            var countryEpidemic = _CountryEpidemicCacheRepository.GetById(countryEpidemicId);

            return View(countryEpidemic);
        }
        
        /// <summary>
        /// Метод получает данные из сессии по ключу
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns>значение из сессии</returns>
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
            _CountryEpidemicCacheRepository.Update(countryEpidemic);

            return View("DistributionStatistics", countryEpidemic);
        }

        /// <summary>
        /// Метод вызывается при нажатии на кнопку PreviousStep
        /// </summary>
        /// <param name="countryEpidemicId">Id эпидемии</param>
        /// <param name="currentDay">День от начала эпидемии</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GoToPreviousStep(string countryEpidemicId, int currentDay)
        {
            var countryEpidemic = _CountryEpidemicCacheRepository.GetById(countryEpidemicId);

            if (currentDay > 1)
            {
                // удаляем статистику за последний день эпидемии
                var lastStatisticNumber = countryEpidemic.DayFromStart - 1;

                for (int i = 0; i < countryEpidemic.CityEpidemics.Count; i++)
                {
                    countryEpidemic.CityEpidemics[i].DiseaseStatistics.RemoveAt(lastStatisticNumber);
                };

                countryEpidemic.DayFromStart = currentDay - 1;

                return View("DistributionStatistics", countryEpidemic);
            }
            else
            {
                //рассчитываем все заново
                _CountryEpidemicParameterSetter.ResetCities(countryEpidemic);

                _CountryEpidemicCacheRepository.Update(countryEpidemic);

                return View("Index", countryEpidemic);
            }
        }

        /// <summary>
        /// Метод вызывается при нажатии на кнопку NextStep
        /// </summary>
        /// <param name="countryEpidemicId">Id эпидемии</param>
        /// <param name="currentDay">День от начала эпидемии</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GoToNextStep(string countryEpidemicId, int currentDay)
        {
            var countryEpidemic = _CountryEpidemicCacheRepository.GetById(countryEpidemicId);

            if (currentDay == countryEpidemic.DayFromStart)
            {
                GetDiseaseStatisticForToday(countryEpidemic);

                _CountryEpidemicCacheRepository.Update(countryEpidemic);
            }
            else
            {
                countryEpidemic.DayFromStart = currentDay + 1;
            }

            return View("DistributionStatistics", countryEpidemic);
        }

        /// <summary>
        /// Метод рассчитывает статистику распространения заболевания для сегодняшнего дня
        /// </summary>
        /// <param name="countryEpidemic">эпидемия с стране</param>
        private void GetDiseaseStatisticForToday(CountryEpidemic countryEpidemic)
        {
            var disease = _DiseaseCacheRepository.GetById(countryEpidemic.DiseaseId);

            foreach (var cityEpidemic in countryEpidemic.CityEpidemics)
            {
                var city = _CityCacheRepository.GetById(cityEpidemic.CityId.ToString());

                var diseaseCitySpread = _DiseaseSpreadFactory.Create(disease, cityEpidemic.DiseaseStatistics, city.Population.PeopleNumber);

                var todaysStatistic = _DiseaseStatisticCalculator.CalculateForToday(diseaseCitySpread, cityEpidemic, city); 

                cityEpidemic.DiseaseStatistics.Add(todaysStatistic);
            }

            countryEpidemic.DayFromStart += 1;
        }
    }
}