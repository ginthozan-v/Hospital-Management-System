using HMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{



    public class DetailPatientViewModel : PatientViewModel
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

    }

    public class CreatePatientViewModel : PatientViewModel
    {
        [DisplayName("Blood Group")]
        [Required(ErrorMessage = "Blood Group Required!")]
        public int BG_ID { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender Required!")]
        public int Gender_ID { get; set; }
    }


    public class PatientViewModel : PatientHealthDetailViewModel
    {
        [Key]
        [DisplayName("ID")]
        public int Patient_ID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Patient Name Required!")]
        public string First_Name { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Patient Name Required!")]
        public string Last_Name { get; set; }

        [DisplayName("Age")]
        [Required(ErrorMessage = "Age Required!")]
        public int? Patient_Age { get; set; }

        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Required!")]
        public string Patient_Phone { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Description = "Email")]
        public string Patient_Email { get; set; }

        [DisplayName("Registered Date")]
        [UIHint("Date")]
        public DateTime Registered_Date { get; set; }

        [DisplayName("Bloodgroup")]
        public string Bloodgroup { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        //public CreateAppointmentModel CreateAppointment { get; set; } = new CreateAppointmentModel();


        [DisplayName("Address")]
        [UIHint("Address")]
        public AddressViewModel Address { get; set; }

        public int PrescriptionId { get; set; }

    }

 
}