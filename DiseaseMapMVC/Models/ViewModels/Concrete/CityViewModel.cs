using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiseaseMapMVC.Models.ViewModels
{
    /// <summary>
    /// Модель представления для добавления города
    /// </summary>
    public class CityViewModel : IViewModel
    {
        /// <summary>
        /// Id города
        /// </summary>
        [ScaffoldColumn(false)]
        public string? Id { get; set; }

        /// <summary>
        /// Id страны
        /// </summary>
        [ScaffoldColumn(false)]
        public string CountryId { get; set; }

        /// <summary>
        /// Название города
        /// </summary>
        [Display(Name = "Name")]
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        /// <summary>
        /// Количество детей
        /// </summary>
        [Display(Name = "KidNumber")]
        [Range(0, int.MaxValue, ErrorMessage = "KidNumberRange")]
        public int KidNumber { get; set; }

        /// <summary>
        /// Количество подростков
        /// </summary>
        [Display(Name = "AdolescentNumber")]
        [Range(0, int.MaxValue, ErrorMessage = "AdolescentNumberRange")]
        public int AdolescentNumber { get; set; }

        /// <summary>
        /// Количество взрослых, работающих граждан
        /// </summary>
        [Display(Name = "AdultNumber")]
        [Range(0, int.MaxValue, ErrorMessage = "AdultNumberRange")]
        public int AdultNumber { get; set; }

        /// <summary>
        /// Количество пенсионеров
        /// </summary>
        [Display(Name = "RetireeNumber")]
        [Range(0, int.MaxValue, ErrorMessage = "RetireeNumberRange")]
        public int RetireeNumber { get; set; }
    }
}
