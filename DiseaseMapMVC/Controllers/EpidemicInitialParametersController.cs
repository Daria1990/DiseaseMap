using DiseaseMongoModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using DiseaseMapMVC.Models;

using DiseaseMapMVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using DiseaseMapMVC.Models.Epidemic;
using MongoDB.Bson;
using DiseaseMapMVC.Resources.Models;

namespace DiseaseMapMVC.Controllers
{
    /// <summary>
    /// Контроллер задания стартовых значений эпидемии
    /// </summary>
    public class EpidemicInitialParametersController : Controller
    {
        /// <summary>
        /// Репозиторий городов
        /// </summary>
        private MongoDbRepository<City> _CityRepository;

        /// <summary>
        /// Репозиторий болезней
        /// </summary>
        private MongoDbRepository<Disease> _DiseaseRepository;

        /// <summary>
        /// Репозиторий стран
        /// </summary>
        private MongoDbRepository<Country> _CountryRepository;
        
        /// <summary>
        /// Репозиторий эпидемии в стране
        /// </summary>
        private MongoDbRepository<CountryEpidemic> _CountryEpidemicRepository;

        /// <summary>
        /// Устанановка значения полей для эпидемии
        /// </summary>
        private ICountryEpidemicParameterSetter _CountryEpidemicParameterSetter;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="context">контекст Mongo</param>
        /// <param name="countryEpidemicParameterSetter">Устанановка значения полей для эпидемии</param>
        public EpidemicInitialParametersController(MongoContext context, ICountryEpidemicParameterSetter countryEpidemicParameterSetter)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _CityRepository = new MongoDbRepository<City>(context);
            _DiseaseRepository = new MongoDbRepository<Disease>(context);
            _CountryRepository = new MongoDbRepository<Country>(context);
            _CountryEpidemicRepository = new MongoDbRepository<CountryEpidemic>(context);

            _CountryEpidemicParameterSetter = countryEpidemicParameterSetter ?? throw new ArgumentNullException(nameof(countryEpidemicParameterSetter));
        }

        public IActionResult Index()
        {
            SetCountryList();
            SetDiseaseList();
            return View();
        }

        /// <summary>
        /// Метод устанавливает параметры для городов, в которых будет эпидемия
        /// </summary>
        /// <param name="epidemicViewModel">модель эпидемии</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetCityParameters(EpidemicViewModel epidemicViewModel)
        {
            if (ModelState.IsValid)
            {
                var countryEpidemic = new CountryEpidemic 
                { 
                    CountryId = epidemicViewModel.CountryId, 
                    DiseaseId = epidemicViewModel.DiseaseId 
                };

                // создаем города, в которых будет эпидемия
                var cities = _CityRepository.SearchFor(o => o.CountryId == new ObjectId(countryEpidemic.CountryId));
                _CountryEpidemicParameterSetter.SetCities(countryEpidemic, cities);

                string id;

                var createSuccess =_CountryEpidemicRepository.Create(countryEpidemic, out id);

                if (createSuccess)
                {
                    // сохраняем id созданной эпидемии в сессии, на всякий случай
                    HttpContext.Session.SetString(ConstantValues.CountryEpidemicSessionKey, id);

                    return RedirectToAction("Index", "EpidemicSpread", new { countryEpidemicId = id });
                }
                else
                {
                    return Content(ErrorMessages.CreateCountryEpidemicError);
                }
            }
            else
            {
                SetCountryList();
                SetDiseaseList();
                return View("Index", epidemicViewModel);
            }
        }

        /// <summary>
        /// Метод необходим для задания выпадающего списка, содержащего болезни, которые могут начать эпидемию
        /// </summary>
        private void SetDiseaseList()
        {
            var diseases = _DiseaseRepository.SearchAll();
            var diseasesShort = diseases.Select(p => new { Name = p.Name, Id = p.Id });
            ViewBag.Diseases = new SelectList(diseasesShort, "Id", "Name");
        }

        /// <summary>
        /// Метод необходим для задания выпадающего списка, содержащего страны, в которых может начаться эпидемия
        /// </summary>
        private void SetCountryList()
        {
            var countries = _CountryRepository.SearchAll();
            var countriesShort = countries.Select(p => new { Name = p.Name, Id = p.Id });
            ViewBag.Countries = new SelectList(countriesShort, "Id", "Name");
        }
    }
}
