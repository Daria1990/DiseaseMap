using DiseaseMapMVC.Models.Converter;
using DiseaseMapMVC.Models.ViewModels;
using DiseaseMapMVC.Resources.Models;
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
    /// <summary>
    /// Контроллер грида городов
    /// </summary>
    public class CityController : DataTableController
    {
        /// <summary>
        /// Репозиторий городов
        /// </summary>
        private MongoDbRepository<City> _CityRepository;

        /// <summary>
        /// Конвертер модели представления города в сущность для работы с базой Mongo
        /// </summary>
        private CityMongoToViewModelConverter<City, CityViewModel> _CityConverter = new CityMongoToViewModelConverter<City, CityViewModel>();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="context">контекст Mongo</param>
        public CityController(MongoContext context)
        {
            _CityRepository = new MongoDbRepository<City>(context);
        }

        public IActionResult Index(string id)
        {
            return View(new Country { Id = id });
        }

        /// <summary>
        /// Метод загружает грид городов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Load(string countryId)
        {
            var data = GetCities(countryId);

            return GetJsonResult<CityViewModel>(data);
        }

        /// <summary>
        /// Метод открывает диалоговое окно для создания города
        /// </summary>
        /// <param name="countryId">id страны, для которой будет создаваться город</param>
        /// <returns></returns>
        public IActionResult Add(string countryId)
        {
            if (!string.IsNullOrEmpty(countryId))
            {
                return PartialView("Editor", new CityViewModel
                {
                    CountryId = countryId
                });
            }

            return Content(ErrorMessages.ErrorPage);
        }

        /// <summary>
        /// Метод редактирует выбранную запись города
        /// </summary>
        /// <param name="id">id города, который будет редактироваться</param>
        /// <returns></returns>
        public IActionResult Edit(string id)
        {
            var editCity = _CityRepository.GetById(id);

            if (editCity != null)
            {
                return PartialView("Editor", _CityConverter.Convert(editCity));
            }

            return Content(ErrorMessages.ErrorPage);
        }

        /// <summary>
        /// Метод обрабатывает нажатие кнопки Сохранить при создании или редактировании города
        /// </summary>
        /// <param name="cityViewModel">модель города, который необходимо создать или отредактировать</param>
        /// <returns></returns>
        public IActionResult Save(CityViewModel cityViewModel)
        {
            if (ModelState.IsValid)
            {
                var changeSuccess = false;

                if (!string.IsNullOrEmpty(cityViewModel.Id))
                {
                    var editCity = _CityRepository.GetById(cityViewModel.Id);

                    _CityConverter.Convert(cityViewModel, editCity);

                    changeSuccess = _CityRepository.Update(editCity);
                }
                else
                {
                    var city = _CityConverter.Convert(cityViewModel);

                    changeSuccess = _CityRepository.Create(city);
                }

                if (changeSuccess)
                {
                    return View("Index", new Country { Id = cityViewModel.CountryId });
                }
                else
                {
                    return Content(ErrorMessages.SaveCityError);
                }
            }
            else
                return PartialView("Editor", cityViewModel);
        }

        /// <summary>
        /// Метод удаляет выбранную запись города
        /// </summary>
        /// <param name="id">id города, который необходимо удалить</param>
        /// <param name="countryId">id страны, в которой находится удаляемый город</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(string id, string countryId)
        {
            var deletedCity = _CityRepository.GetById(id);

            if (deletedCity != null)
            {
                _CityRepository.Delete(deletedCity);
            }

            var data = GetCities(countryId);

            return GetJsonResult<CityViewModel>(data);
        }

        /// <summary>
        ///  Метод получает список записей, исходя из значения, введеного в строку поиска
        /// </summary>
        /// <param name="countryId">id станы, в которой находятся города</param>
        /// <returns></returns>
        private IEnumerable<CityViewModel> GetCities(string countryId)
        {
            var searchValue = GetSearchValue();

            var cities = _CityRepository.SearchFor(e => e.CountryId == new ObjectId(countryId) && e.Name.ToLower().Contains(searchValue));

            return cities.Select(c => _CityConverter.Convert(c));
        }
    }
}
