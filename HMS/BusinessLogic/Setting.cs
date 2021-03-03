using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.BusinessLogic
{

    public class Setting
    {
        private HMSEntities db = new HMSEntities();

        //Get DateFormat from db
        public string GetDateFormate()
        {
            var dateformate = (from s in db.Settings
                        join df in db.DateFormats on s.DF_ID equals df.DF_ID
                        where s.Name == "DateFormat"
                        select df.Format).FirstOrDefault();
                         
            return dateformate;
        }
    }
}