//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            this.Prescriptions = new HashSet<Prescription>();
        }
    
        public int Appointment_ID { get; set; }
        public int Patient_ID { get; set; }
        public int Doctor_ID { get; set; }
        public int Desease_ID { get; set; }
        public int Appointment_Status { get; set; }
        public int Appointment_Date { get; set; }
        public System.TimeSpan Appointment_Time { get; set; }
    
        public virtual Appointment Appointment1 { get; set; }
        public virtual Appointment Appointment2 { get; set; }
        public virtual Desease Desease { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual TimeSlot TimeSlot { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
