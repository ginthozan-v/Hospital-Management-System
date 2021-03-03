using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class DeseasesDoctorsViewModel
    {
        [DisplayName("ID")]
        public int dd_ID { get; set; }

        [DisplayName("Desease")]
        [Required(ErrorMessage = "Desease Required!")]
        public int Desease_ID { get; set; }

        [DisplayName("Doctor")]
        [Required(ErrorMessage = "Doctor Required!")]
        public int Doctor_ID { get; set; }

        [DisplayName("Desease")]
        public String Desease { get; set; }

        [DisplayName("Doctor")]
        public String Doctor { get; set; }
    }
}