using DiseaseMongoModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using DiseaseMapMVC.Models;

using DiseaseMapMVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace DiseaseMapMVC.Controllers
{
    public class EpidemicInitialParametersController : Controller
    {
        private MongoDbRepository<Disease> _diseaseRepository;
        private MongoDbRepository<Country> _countryRepository;
        private MongoDbRepository<City> _cityRepository;
        private MongoDbRepository<CountryEpidemic> _countryEpidemicRepository;

        public EpidemicInitialParametersController(MongoContext context)
        {
            _diseaseRepository = new MongoDbRepository<Disease>(context);
            _countryRepository = new MongoDbRepository<Country>(context);
            _cityRepository = new MongoDbRepository<City>(context);
            _countryEpidemicRepository = new MongoDbRepository<CountryEpidemic>(context);
        }

        public IActionResult Index()
        {
            SetCountryList();
            SetDiseaseList();
            return View();
        }

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

                var countryEidemicSetter = new CountryEidemicParameterSetter(countryEpidemic);

                var cities = _cityRepository.SearchFor(o => o.CountryId == new ObjectId(countryEpidemic.CountryId));
                countryEpidemic = countryEidemicSetter.SetCities(cities);

                string id;

                _countryEpidemicRepository.Create(countryEpidemic, out id);

                SetDataToHttpContext(ConstantValues.SessionKey.COUNTRY_EPIDEMIC_ID_KEY, id);

                return RedirectToAction("Index", "EpidemicSpread", new { countryEpidemicId = id });
            }
            else
            {
                SetCountryList();
                SetDiseaseList();
                return View("Index", epidemicViewModel);
            }
        }

        private void SetDataToHttpContext(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        private void SetDiseaseList()
        {
            var diseases = _diseaseRepository.SearchAll();
            var diseasesShort = diseases.Select(p => new { Name = p.Name, Id = p.Id });
            ViewBag.Diseases = new SelectList(diseasesShort, "Id", "Name");
        }

        private void SetCountryList()
        {
            var countries = _countryRepository.SearchAll();
            var countriesShort = countries.Select(p => new { Name = p.Name, Id = p.Id });
            ViewBag.Countries = new SelectList(countriesShort, "Id", "Name");
        }
    }
}
