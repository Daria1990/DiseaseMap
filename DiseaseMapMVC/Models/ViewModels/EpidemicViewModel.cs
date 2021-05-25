using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.ViewModels
{
    public class EpidemicViewModel : IViewModel
    {
        [Display(Name = "DiseaseName")]
        [Required(ErrorMessage = "DiseaseNameRequired")]
        public string DiseaseId { get; set; }

        [Display(Name = "CountryName")]
        [Required(ErrorMessage = "CountryNameRequired")]
        public string CountryId { get; set; }
    }
}
