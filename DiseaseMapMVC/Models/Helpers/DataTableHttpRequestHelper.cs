using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DiseaseMapMVC.Models.Helpers
{
    /// <summary>
    /// Вспомогательный класс для работы с запросом грида DataTable
    /// </summary>
    public class DataTableHttpRequestHelper
    {
        public int GetPageSize(HttpRequest request)
        {
            var length = request.Form["length"].FirstOrDefault();

            return length != null ? Convert.ToInt32(length) : 0;
        }

        public string GetSearchValue(HttpRequest request)
        {
            var searchValue = request.Form["search[value]"].FirstOrDefault();

            if (searchValue == null)
            {
                return string.Empty;
            }
            else
            {
                return searchValue.ToLower();
            }
        }

        public int GetNumberOfSkipRows(HttpRequest request)
        {
            var start = request.Form["start"].FirstOrDefault();

            return start != null ? Convert.ToInt32(start) : 0;
        }

        public int GetDraw(HttpRequest request)
        {
            var draw = request.Form["draw"].FirstOrDefault();

            return draw != null ? Convert.ToInt32(draw) : 0;
        }
    }
}
