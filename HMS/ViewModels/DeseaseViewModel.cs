using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class DeseaseViewModel
    {
        [DisplayName("Desease ID")]
        public int Desease_ID { get; set; }

        [DisplayName("Desease")]
        [Required(ErrorMessage = "Desease Required!")]
        public string Desease { get; set; }

        public DateTime Date { get; set; }
    }
}