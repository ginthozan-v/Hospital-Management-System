using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class SettingViewModel
    {
        public string Name { get; set; }
        public int DF_ID { get; set; }

        public virtual DateFormat DateFormat { get; set; }
    }
}