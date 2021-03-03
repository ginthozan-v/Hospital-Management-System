using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using HMS.Models;
using System.ComponentModel.DataAnnotations;

namespace HMS.ViewModels
{
    public class ConfigurationViewModel
    {
   
    }

    public class ShiftViewModel { 

        //ShitPattern
        [Key]
        public int ID { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [UIHint("Time")]
        [DisplayName("Start Time")]
        public TimeSpan StartTime { get; set; }

    }
}