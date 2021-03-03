using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.MVCHelper
{
    public static class DatePicker
    {
        //DisplayDateFormat
        public static MvcHtmlString DisplayDate(this HtmlHelper htmlHelper, DateTime date)
        {
            string datein = date.ToString(MvcApplication.DATEFORMAT);
            return new MvcHtmlString(datein);
        }
    }
}