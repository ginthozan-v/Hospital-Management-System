using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class CreatePrescriptionViewModel : PrescriptionViewModel
    {

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        [DisplayName("Drug")]
        [Required(ErrorMessage = "Drug Required!")]
        public string Drug_Name { get; set; }
        public string Quantity { get; set; }

        public string Qty { get; set; }

        [DataType(DataType.MultilineText)]
        public string Instruction { get; set; }

        [UIHint("Date")]
        [DataType(DataType.Date)]
        public System.DateTime DateTime { get; set; }

    }
    public class PrescriptionViewModel
    {
        public string Patient { get; set; }
        public int PrescriptionId { get; set; }
        public int? AppointmentId { get; set; }

        public string Desease { get; set; }

        [DisplayName("Dosage")]
        public string Dosage { get; set; }

        [DisplayName("Duration")]
        public string Duration { get; set; }

        public string Card { get; set; }
        public string Tablet { get; set; }

        [DisplayName("Repeat")]
        public string Repeat { get; set; }

        [DisplayName("Time of the day")]
        public string TimeOfDay { get; set; }

        [DisplayName("To be taken")]
        public string Taken { get; set; }



        //Days
        [DisplayName("Mon")]
        public bool Mon { get; set; }

        [DisplayName("Tue")]
        public bool Tue { get; set; }

        [DisplayName("Wed")]
        public bool Wed { get; set; }

        [DisplayName("Thu")]
        public bool Thu { get; set; }

        [DisplayName("Fri")]
        public bool Fri { get; set; }

        [DisplayName("Sat")]
        public bool Sat { get; set; }

        [DisplayName("Sun")]
        public bool Sun { get; set; }

        public string Specificday { get; set; }


        //Timeofday

        [DisplayName("Morning")]
        public bool Morning { get; set; }

        [DisplayName("Noon")]
        public bool Noon { get; set; }

        [DisplayName("Evening")]
        public bool Evening { get; set; }

        [DisplayName("Night")]
        public bool Night { get; set; }

        [DisplayName("Before food")]
        public int Beforefood { get; set; }

        [DisplayName("After food")]
        public int Afterfood { get; set; }

        [UIHint("Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Datee { get; set; }
        public TimeSpan Dur { get; set; }

  

        public int? Years { get; set; }
        public int? Months { get; set; }
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public int? Minutes { get; set; }
        public int? Seconds { get; set; }
        public int Milliseconds { get; set; }
    }



}