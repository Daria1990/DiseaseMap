using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiseaseMapMVC.Models.Converter;
using DiseaseMapMVC.Models.ViewModels;
using DiseaseMongoModel;
using Newtonsoft.Json;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;

namespace DiseaseMapMVC.Controllers
{
    public class CountryController : DataTableController
    {
        private MongoDbRepository<Country> _countryRepository;

        private readonly CountryMongoToViewModelConverter<Country, CountryViewModel> _countryConverter = new CountryMongoToViewModelConverter<Country, CountryViewModel>();

        public CountryController(MongoContext context)
        {
            _countryRepository = new MongoDbRepository<Country>(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Load()
        {
            var data = GetCountries();

            return GetJsonResult<Country>(data);
        }

        public IActionResult Save(CountryViewModel countryViewModel)
        {
            if (ModelState.IsValid)
            {
                var changeSuccess = false;

                if (!string.IsNullOrEmpty(countryViewModel.Id))
                {
                    var editCountry = _countryRepository.GetById(countryViewModel.Id);

                    editCountry = _countryConverter.Convert(countryViewModel, editCountry);

                    changeSuccess = _countryRepository.Update(editCountry);
                }
                else
                {
                    var country = _countryConverter.Convert(countryViewModel);

                    changeSuccess = _countryRepository.Create(country);
                }

                if (changeSuccess)
                {
                    return View("Index");
                }
                else
                {
                    return Content("error");
                }
            }
            else
                return PartialView("Editor", countryViewModel);
        }

        public IActionResult Add()
        {
            return PartialView("Editor");
        }

        public IActionResult Edit(string id)
        {
            var editCountry = _countryRepository.GetById(id);

            if (editCountry != null)
                return PartialView("Editor", _countryConverter.Convert(editCountry));

            return Content("ErrorPage");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var deletedCountry = _countryRepository.GetById(id);

            if (deletedCountry != null)
            {
                _countryRepository.Delete(deletedCountry);
            }

            var data = GetCountries();

            return GetJsonResult<Country>(data);
        }

        private IEnumerable<Country> GetCountries()
        {
            // Search Value from (Search box)  
            var searchValue = GetSearchValue();

            return _countryRepository.SearchFor(e => e.Name.ToLower().Contains(searchValue));
        }
    }
}
