using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class HospitalViewModel
    {
        [DisplayName("ID")]
        public int Hospital_ID { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name Required!")]
        public string Hospital_Name { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address Required!")]
        public string Hospital_Address { get; set; }

        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Required!")]
        public string Hospital_Phone { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Required!")]
        public string Hospital_Email { get; set; }
    }
}