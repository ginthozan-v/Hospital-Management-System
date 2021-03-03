using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class PatientHealthDetailViewModel 
    {
        public int PatientId { get; set; }
        public int Ph_Id { get; set; }

        [DisplayName("Height")]
        public int? Height { get; set; }

        [DisplayName("Weight")]
        public int? Weight { get; set; }

        [DisplayName("Heart Rate")]
        public int? HeartRate { get; set; }

        [DisplayName("Body Temperature")]
        public int? BodyTemperature { get; set; }

        [DisplayName("Glucose")]
        public int? Glucose { get; set; }

        [DisplayName("Allergies")]
        public string Allergies { get; set; }

        [DisplayName("Diseases")]
        public string Diseases { get; set; }

        [DisplayName("LastVisit")]
        [UIHint("Date")]
        [DataType(DataType.Date)]
        public DateTime LastVisit { get; set; }
        public string LastVisitString { get; set; }
    }
}