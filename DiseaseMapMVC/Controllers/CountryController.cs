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
using DiseaseMapMVC.Resources.Models;

namespace DiseaseMapMVC.Controllers
{
    /// <summary>
    /// Контроллер грида стран
    /// </summary>
    public class CountryController : DataTableController
    {
        /// <summary>
        /// Репозиторий стран
        /// </summary>
        private MongoDbRepository<Country> _СountryRepository;

        /// <summary>
        /// Конвертер модели представления страны в сущность для работы с базой Mongo
        /// </summary>
        private CountryMongoToViewModelConverter<Country, CountryViewModel> _СountryConverter = new CountryMongoToViewModelConverter<Country, CountryViewModel>();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="context">контекст Mongo</param>
        public CountryController(MongoContext context)
        {
            _СountryRepository = new MongoDbRepository<Country>(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Метод загружает грид стран
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Load()
        {
            var data = GetSearchCountries();

            return GetJsonResult<Country>(data);
        }

        /// <summary>
        /// Метод обрабатывает нажатие кнопки Сохранить при создании или редактировании стран
        /// </summary>
        /// <param name="countryViewModel">модель страны, которую необходимо создать или отредактировать</param>
        /// <returns></returns>
        public IActionResult Save(CountryViewModel countryViewModel)
        {
            if (ModelState.IsValid)
            {
                var changeSuccess = false;

                if (!string.IsNullOrEmpty(countryViewModel.Id))
                {
                    var editCountry = _СountryRepository.GetById(countryViewModel.Id);

                    _СountryConverter.Convert(countryViewModel, editCountry);

                    changeSuccess = _СountryRepository.Update(editCountry);
                }
                else
                {
                    var country = _СountryConverter.Convert(countryViewModel);

                    changeSuccess = _СountryRepository.Create(country);
                }

                if (changeSuccess)
                {
                    return View("Index");
                }
                else
                {
                    return Content(ErrorMessages.SaveCountryError);
                }
            }
            else
                return PartialView("Editor", countryViewModel);
        }

        /// <summary>
        /// Метод открывает диалоговое окно для создания страны
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return PartialView("Editor");
        }

        /// <summary>
        /// Метод редактирует выбранную запись страны
        /// </summary>
        /// <param name="id">id страны, которая будет редактироваться</param>
        /// <returns></returns>
        public IActionResult Edit(string id)
        {
            var editCountry = _СountryRepository.GetById(id);

            if (editCountry != null)
            {
                return PartialView("Editor", _СountryConverter.Convert(editCountry));
            }

            return Content(ErrorMessages.ErrorPage);
        }

        /// <summary>
        /// Метод удаляет выбранную запись страны
        /// </summary>
        /// <param name="id">id страны, которую необходимо удалить</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var deletedCountry = _СountryRepository.GetById(id);

            if (deletedCountry != null)
            {
                _СountryRepository.Delete(deletedCountry);
            }

            var data = GetSearchCountries();

            return GetJsonResult<Country>(data);
        }
        
        /// <summary>
        /// Метод получает список записей, исходя из значения, введеного в строку поиска
        /// </summary>
        /// <returns>список записей</returns>
        private IEnumerable<Country> GetSearchCountries()
        {
            var searchValue = GetSearchValue();

            return _СountryRepository.SearchFor(e => e.Name.ToLower().Contains(searchValue));
        }
    }
}
