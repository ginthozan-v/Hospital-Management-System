using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace HMS.ViewModels
{
    public class InpatientViewModel
    {

        public int Id { get; set; }
        public Nullable<int> patientsId { get; set; }
        public Nullable<int> Ward_No { get; set; }
        public System.DateTime AdmissionDate { get; set; }
        public Nullable<System.DateTime> DischargeDate { get; set; }

        public string PatientName { get; set; }

        public int WardNo { get; set; }

    }
}