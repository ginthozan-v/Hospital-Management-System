using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using HMS.ViewModels;
using HMS.BusinessLogic;

namespace HMS.ViewModels
{
    public class DetailDoctorViewModel : DoctorViewModel
    {
        [DisplayName("Street")]
        public string Street { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Province")]
        public string Province { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; }

        [DisplayName("Post Code")]
        public string Post_Code { get; set; }

        [DisplayName("Bloodgroup")]
        public string Bloodgroup { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }
    }

    public class CreateDoctorViewModel : DoctorViewModel
    {
        [DisplayName("Specialization")]
        [Required(ErrorMessage = "Specialization Required!")]
        public int Specialization_ID { get; set; }


        [DisplayName("Bloodgroup")]
        [Required(ErrorMessage = "Bloodgroup Required!")]
        public int Bloodgroup_ID { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender Required!")]
        public int Gender_ID { get; set; }
    }

    [Serializable()]
    public class DoctorViewModel
    {
        [Key]
        [DisplayName("ID")]
        public int Doctor_ID { get; set; }

        [DisplayName("Specialization")]
        public string Specialization { get; set; }

        [DisplayName("Bloodgroup")]
        public string BloodGroup { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name Required!")]
        public string First_Name { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name Required!")]
        public string Last_Name { get; set; }

        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number Required!")]
        public string Doctor_Phone { get; set; }

        [DisplayName("Age")]
        [Required(ErrorMessage = "Age Required!")]
        public string Doctor_Age { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Required!")]
        public string Doctor_Email { get; set; }

        [UIHint("Status")]
        [DisplayName("Status")]
        public int Doctor_Status { get; set; }

        [DisplayName("Gender")]
        public string Doctor_Gender { get; set; }

        [UIHint("Date")]
        public DateTime Registered_Date { get; set; }

        [DisplayName("Address")]
        [UIHint("Address")]
        public AddressViewModel Address { get; set; }

        public string ImagePath { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }

        public string Fullname { get; set; }
    }
}