using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace HMS.ViewModels
{
    public class CreateWardModel : WardViewModel
    {
        public int Hospital_ID { get; set; }
    }

    public class WardViewModel
    {
        [DisplayName("Ward No")]
        public int Ward_No { get; set; }

        [DisplayName("Status")]
        public int Ward_Status { get; set; }

    }
}