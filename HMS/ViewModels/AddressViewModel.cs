using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Street")]
        [Required(ErrorMessage = "Street Required!")]
        public string Street { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "City Required!")]
        public string City { get; set; }

        [DisplayName("Province")]
        [Required(ErrorMessage = "Province")]
        public string Province { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Country")]
        public string Country { get; set; }

        [DisplayName("Post Code")]
        [Required(ErrorMessage = "Post Code Required!")]
        public string Post_Code { get; set; }
    }
}