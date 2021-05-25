using DiseaseMapMVC.Models.Converter;
using DiseaseMapMVC.Models.Helpers;
using DiseaseMapMVC.Models.ViewModels;
using DiseaseMongoModel;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Controllers
{
    public class CityController : DataTableController
    {
        private MongoDbRepository<City> _cityRepository;

        private CityMongoToViewModelConverter<City, CityViewModel> _cityConverter = new CityMongoToViewModelConverter<City, CityViewModel>();

        public CityController(MongoContext context)
        {
            _cityRepository = new MongoDbRepository<City>(context);
        }

        public IActionResult Index(string id)
        {
            return View(new Country { Id = id });
        }

        [HttpPost]
        public IActionResult Load(string countryId)
        {
            var data = GetCities(countryId);

            return GetJsonResult<CityViewModel>(data);
        }

        public IActionResult Add(string countryId)
        {
            if (!string.IsNullOrEmpty(countryId))
            {
                return PartialView("Editor", new CityViewModel
                {
                    CountryId = countryId
                });
            }

            return Content("ErrorPage");
        }

        public IActionResult Edit(string id)
        {
            var editCity = _cityRepository.GetById(id);

            if (editCity != null)
                return PartialView("Editor", _cityConverter.Convert(editCity));

            return Content("ErrorPage");
        }

        public IActionResult Save(CityViewModel cityViewModel)
        {
            if (ModelState.IsValid)
            {
                var changeSuccess = false;

                if (!string.IsNullOrEmpty(cityViewModel.Id))
                {
                    var editCity = _cityRepository.GetById(cityViewModel.Id);

                    editCity = _cityConverter.Convert(cityViewModel, editCity);

                    changeSuccess = _cityRepository.Update(editCity);
                }
                else
                {
                    var city = _cityConverter.Convert(cityViewModel);

                    changeSuccess = _cityRepository.Create(city);
                }

                if (changeSuccess)
                    return View("Index", new Country { Id = cityViewModel.CountryId });
                else
                    return Content("error");
            }
            else
                return PartialView("Editor", cityViewModel);
        }

        [HttpPost]
        public IActionResult Delete(string id, string countryId)
        {
            var deletedCity = _cityRepository.GetById(id);

            if (deletedCity != null)
            {
                _cityRepository.Delete(deletedCity);
            }

            var data = GetCities(countryId);

            return GetJsonResult<CityViewModel>(data);
        }

        private IEnumerable<CityViewModel> GetCities(string countryId)
        {
            // Search Value from (Search box)  
            var searchValue = GetSearchValue();

            var cities = _cityRepository.SearchFor(e => e.CountryId == new ObjectId(countryId) && e.Name.ToLower().Contains(searchValue));

            return cities.Select(c => _cityConverter.Convert(c));
        }
    }
}
