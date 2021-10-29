using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Controllers
{
    /// <summary>
    /// Вспомогательный контроллер для работы с табличными данными
    /// </summary>
    public abstract class DataTableController : Controller
    {
        /// <summary>
        /// Метод получает количество записей на странице таблицы
        /// </summary>
        /// <typeparam name="T">тип данных</typeparam>
        /// <param name="data">данные в таблице</param>
        /// <returns>данные на странице</returns>
        protected IEnumerable<T> GetRecordsOnPage<T>(IEnumerable<T> data)
        {
            var start = Request.Form["start"].FirstOrDefault();
            var skip = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : 0;

            var length = Request.Form["length"].FirstOrDefault();
            var pageSize = !string.IsNullOrEmpty(length) ? Convert.ToInt32(length) : 0;

            return data.Skip(skip).Take(pageSize);
        }

        /// <summary>
        /// Метод получает значение введеное в строке поиска
        /// </summary>
        /// <returns>поисковое значение</returns>
        protected string GetSearchValue()
        {
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            return !string.IsNullOrEmpty(searchValue) ? searchValue.ToLower() : string.Empty;
        }

        /// <summary>
        /// Метод получает результат работы с таблицей
        /// </summary>
        /// <typeparam name="T">тип данных</typeparam>
        /// <param name="data">данные в таблице</param>
        /// <returns></returns>
        protected JsonResult GetJsonResult<T>(IEnumerable<T> data)
        {
            var draw = Request.Form["draw"].FirstOrDefault();

            return new JsonResult(new JqueryDataTablesResult<T>
            {
                Draw = !string.IsNullOrEmpty(draw) ? Convert.ToInt32(draw) : 0,
                Data = GetRecordsOnPage<T>(data),
                RecordsFiltered = data.Count(),
                RecordsTotal = data.Count()
            });
        }
    }
}
