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
    
    public partial class TimeSlot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeSlot()
        {
            this.Appointments = new HashSet<Appointment>();
        }
    
        public int TimeID { get; set; }
        public int Doctor_ID { get; set; }
        public System.DateTime Date { get; set; }
        public int SID { get; set; }
        public int Duration { get; set; }
        public int Interval { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ShiftPattern ShiftPattern { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}