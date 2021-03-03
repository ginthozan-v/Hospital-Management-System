using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class CheckoutViewModel
    {
        public int Id { get; set; }

        public int PrescriptionId { get; set; }

        [DisplayName("Patient name")]
        public string PatientName{ get; set; }

        [DisplayName("Name on card")]
        public string NameOnCard { get; set; }

        [DisplayName("Card number")]
        public int? CardNumber { get; set; }

        [DisplayName("EXP")]
        public int? EXP { get; set; }

        [DisplayName("CCV")]
        public int? CCV { get; set; }

        [DisplayName("Total")]
        public int Amount { get; set; }

        public string PaymentOption { get; set; }
    }
}