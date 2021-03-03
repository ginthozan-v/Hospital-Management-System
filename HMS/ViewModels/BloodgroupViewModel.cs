using HMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class BloodgroupViewModel
    {
        [DisplayName("Bloodgroup ID")]
        public int ID { get; set; }

        [DisplayName("Bloodgroup")]
        [Required(ErrorMessage = "Bloodgroup Required!")]
        public string Bloodgroup { get; set; }
    }
}