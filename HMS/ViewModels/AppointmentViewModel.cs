using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{

    public class CreateAppointmentModel : AppointmentViewModel
    {
        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Patient Name Required!")]
        public int Patient_ID { get; set; }

        [DisplayName("Doctor Name")]
        [Required(ErrorMessage = "Doctor Name Required!")]
        public int Doctor_ID { get; set; }

        [DisplayName("Desease")]
        [Required(ErrorMessage = "Desease Required!")]

        public int Desease_ID { get; set; }

        [DisplayName("Appointment Date")]
        public int Appointment_Date_Id { get; set; }
    }


    public class AppointmentViewModel
    {
        [Key]
        [DisplayName("Appointment NO")]
        public int Appointment_ID { get; set; }

        public int TimeID { get; set; }

        [DisplayName("Patient Name")]
        public string Patient_Name { get; set; }

        [DisplayName("Doctor Name")]
        public string Doctor_Name { get; set; }

        [DisplayName("Doctor Name")]
        [Required(ErrorMessage = "Doctor Name Required!")]
        public int Doctor_ID { get; set; }

        [DisplayName("Desease")]
        public string Desease { get; set; }

        [UIHint("Status")]
        [DisplayName("Appointment Status")]
        public int Appointment_Status { get; set; }

        [UIHint("Date")]
        [DisplayName("Appointment Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date Required!")]
        public DateTime Appointment_Date { get; set; }

        [DisplayName("Time")]
        //[UIHint("TimeSpan")]
        public TimeSpan Appointment_Time { get; set; }

        public string App_Date { get; set; }

        public int? Duration { get; set; }

        public int Hour { get; set; }

        [DisplayName("Interval")]
        public int Interval { get; set; }
        public string Contact { get; set; }
        public string ProfilePic { get; set; }

        public int PatientId { get; set; }
    }

}