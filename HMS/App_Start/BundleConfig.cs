using System.Web;
using System.Web.Optimization;

namespace HMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/popper.js",
                         "~/Scripts/moment.js",
                         "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/src/Main.js",
                      "~/Scripts/MyScripts/custom.js",
                      "~/Scripts/MyScripts/ModalPopup.js",
                      "~/Scripts/jquery-ui-1.12.1.min.js",
                      "~/Scripts/jquery-ui-timepicker-addon.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/bootstrap.min.css",
                      "~/Content/Site.css",
                      "~/Content/main.css",
                      "~/Content/font-awesome/all.css",
                      "~/Content/themes/base/jquery-ui.min.css",
                      "~/Content/jquery-ui-timepicker-addon.min.css"
                      ));
        }
    }
}
