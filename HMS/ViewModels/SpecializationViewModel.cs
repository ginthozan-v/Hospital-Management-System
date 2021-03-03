using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class SpecializationViewModel
    {
        [DisplayName("ID")]
        public int Specialization_ID { get; set; }

        [DisplayName("Specialization")]
        [Required(ErrorMessage = "Specialization Name Required!")]
        public string Specialization { get; set; }
    }
}