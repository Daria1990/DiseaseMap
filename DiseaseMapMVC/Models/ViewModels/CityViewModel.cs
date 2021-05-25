using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiseaseMapMVC.Models.ViewModels
{
    public class CityViewModel : IViewModel
    {
        [ScaffoldColumn(false)]
        public string? Id { get; set; }

        [ScaffoldColumn(false)]
        public string CountryId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

        [Display(Name = "KidNumber")]
        [Range(0, int.MaxValue, ErrorMessage = "KidNumberRange")]
        public int KidNumber { get; set; }

        [Display(Name = "AdolescentNumber")]
        [Range(0, int.MaxValue, ErrorMessage = "AdolescentNumberRange")]
        public int AdolescentNumber { get; set; }

        [Display(Name = "AdultNumber")]
        [Range(0, int.MaxValue, ErrorMessage = "AdultNumberRange")]
        public int AdultNumber { get; set; }

        [Display(Name = "RetireeNumber")]
        [Range(0, int.MaxValue, ErrorMessage = "RetireeNumberRange")]
        public int RetireeNumber { get; set; }
    }
}
