using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Controllers
{
    /// <summary>
    /// Контроллер страницы ошибок
    /// </summary>
    public class ErrorController : Controller
    {
        [HttpGet("/Error/{statusCode}")]
        public IActionResult Index(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404 || statusCode.Value == 500)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }
            return View();
        }
    }
}
