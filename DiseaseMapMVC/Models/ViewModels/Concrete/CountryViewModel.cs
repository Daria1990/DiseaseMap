using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.ViewModels
{
    /// <summary>
    /// Модель представления для добавления страны
    /// </summary>
    public class CountryViewModel : IViewModel
    {
        /// <summary>
        /// Id страны
        /// </summary>
        [ScaffoldColumn(false)]
        public string? Id { get; set; }

        /// <summary>
        /// Название страны
        /// </summary>
        [Display(Name = "Name")]
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

    }
}
