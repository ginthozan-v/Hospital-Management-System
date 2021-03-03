using HMS.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HMS
{
    public class MvcApplication : HttpApplication
    {
        public static string DATEFORMAT = "";

        //Make Dateformat Global
        public static void SetDateGlobal()
        { 
            Setting setting = new Setting();
            DATEFORMAT = setting.GetDateFormate();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SetDateGlobal();

        }

        protected void Application_EndRequest()
        {
            var errors = this.Context.AllErrors;  //Array of errors
        }

        protected void Application_BeginRequest()
        {
            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            info.DateTimeFormat.ShortDatePattern = DATEFORMAT;
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
        }

    }
}
