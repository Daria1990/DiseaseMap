using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.ViewModels
{
    /// <summary>
    /// Модель представления для создания эпидемии
    /// </summary>
    public class EpidemicViewModel : IViewModel
    {
        /// <summary>
        /// Id болезни
        /// </summary>
        [Display(Name = "DiseaseName")]
        [Required(ErrorMessage = "DiseaseNameRequired")]
        public string DiseaseId { get; set; }

        /// <summary>
        /// Id страны, в которой будет происходить эпидемия
        /// </summary>
        [Display(Name = "CountryName")]
        [Required(ErrorMessage = "CountryNameRequired")]
        public string CountryId { get; set; }
    }
}
