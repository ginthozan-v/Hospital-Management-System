using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HMS.ViewModels
{
    public class CreateTimeSlotModel : TimeSlotViewModel
    {
        [Required]
        public int Doctor_ID { get; set; }
    }

    public class TimeSlotViewModel
    {

        [DisplayName("ID")]
        public int TimeID { get; set; }

        [DisplayName("Doctor")]
        public string Doctor { get; set; }

        public string ProfilePic { get; set; }

        public int Status { get; set; }

        [DisplayName("Specialization")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Time Required!")]
        [DisplayName("Time")]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Day Required!")]
        [DisplayName("Day")]
        [UIHint("Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayName("Hours")]
        [Required(ErrorMessage = "Hours Required!")]
        public int Hours { get; set; }

        [DisplayName("Interval")]
        [Required(ErrorMessage = "Interval")]
        public int Interval { get; set; }

        [Required(ErrorMessage = "Shift Pattern Required!")]
        [DisplayName("Shift Pattern")]
        public int SID { get; set; }


        //DateRange
        [Required(ErrorMessage = "Start Date Required!")]
        [DisplayName("Start Date")]
        [UIHint("DateRange")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date Required!")]
        [DisplayName("End Date")]
        [UIHint("DateRange")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }


        //TimeRange
        [DisplayName("Start Time")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan StartTime { get; set; }

        [DisplayName("End Time")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan EndTime { get; set; }

        //Weeks
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
    }



















    //[EdmEntityTypeAttribute(NamespaceName = "ViewModels", Name="Weeks")]
    //[Serializable()]
    //[DataContractAttribute(IsReference= true)]
    //public partial class Weeks : CreateAppointmentModel     
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public bool isSelected;
    //}


}