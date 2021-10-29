﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DiseaseMapMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using DiseaseMongoModel;

namespace DiseaseMapMVC.Controllers
{
    /// <summary>
    /// Контроллер заглавной страницы
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CalcEpidemicSpread()
        {
            return RedirectToAction("Index", "EpidemicInitialParameters");
        }

        public IActionResult SetCountryParameters()
        {
            return RedirectToAction("Index", "Country");
        }

        public IActionResult SetLanguage(string culture, string returnUrl, string queryString)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)  }
            );

            return LocalRedirect(returnUrl + queryString);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
