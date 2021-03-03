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
    
    public partial class Doctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctor()
        {
            this.Appointments = new HashSet<Appointment>();
            this.AspNetUsers = new HashSet<AspNetUser>();
            this.Deseases_Doctors = new HashSet<Deseases_Doctors>();
            this.Prescriptions = new HashSet<Prescription>();
            this.TimeSlots = new HashSet<TimeSlot>();
        }
    
        public int Doctor_ID { get; set; }
        public int Spcl_ID { get; set; }
        public int BG_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Doctor_Phone { get; set; }
        public string Doctor_Age { get; set; }
        public string Doctor_Email { get; set; }
        public int Doctor_Status { get; set; }
        public int Gender_ID { get; set; }
        public System.DateTime Registered_Date { get; set; }
        public int AddressID { get; set; }
        public string ImagePath { get; set; }
    
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual Bloodgroup Bloodgroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deseases_Doctors> Deseases_Doctors { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Specialization Specialization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
    }
}