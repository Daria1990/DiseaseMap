using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.ViewModels
{
    public class CountryViewModel : IViewModel
    {
        [ScaffoldColumn(false)]
        public string? Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "NameRequired")]
        public string Name { get; set; }

    }
}
