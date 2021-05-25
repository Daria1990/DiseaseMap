using DiseaseMapMVC.Models.Helpers;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Controllers
{
    public abstract class DataTableController : Controller
    {
        private DataTableHttpRequestHelper _httpRequestHelper = new DataTableHttpRequestHelper();

        protected IEnumerable<T> GetRecordsOnPage<T>(IEnumerable<T> data)
        {
            var skip = _httpRequestHelper.GetNumberOfSkipRows(Request);

            var pageSize = _httpRequestHelper.GetPageSize(Request);

            return data.Skip(skip).Take(pageSize);
        }

        protected string GetSearchValue()
        {
            return _httpRequestHelper.GetSearchValue(Request);
        }

        protected JsonResult GetJsonResult<T>(IEnumerable<T> data)
        {
            return new JsonResult(new JqueryDataTablesResult<T>
            {
                Draw = _httpRequestHelper.GetDraw(Request),
                Data = GetRecordsOnPage<T>(data),
                RecordsFiltered = data.Count(),
                RecordsTotal = data.Count()
            });
        }
    }
}
