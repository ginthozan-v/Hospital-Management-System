using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class GenderViewModel
    {
        [DisplayName("GENDER ID")]
        public int Gender_ID { get; set; }

        [DisplayName("GENDER")]
        [Required(ErrorMessage = "Gender Name Required")]
        public string Gender { get; set; }
    }
}