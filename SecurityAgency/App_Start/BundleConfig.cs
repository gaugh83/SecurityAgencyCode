using System.Web;
using System.Web.Optimization;

namespace ModelMarket
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/SecurityCss").Include(
                "~/Content/css/bootstrap.min.css",

                "~/Content/css/bootstrap-override.css",
                "~/Content/css/weather-icons.min.css",
                "~/Content/css/jquery-ui-1.10.3.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/animate.min.css",
                "~/Content/css/animate.delay.css",
                "~/Content/css/toggles.css",
                "~/Content/css/pace.css",
                "~/Content/css/style.default.css",
                "~/Content/Custome.css",
                "~/Content/alertify.core.css",
                "~/Content/alertify.default.css",
                "~/Content/DataTable/jquery.dataTables.min.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/js/CommonJS").Include(
                       "~/Scripts/js/jquery-1.11.1.min.js",
                       "~/Scripts/js/jquery-migrate-1.2.1.min.js",
                       "~/Scripts/js/jquery-ui-1.10.3.min.js",
                       "~/Scripts/js/bootstrap.min.js",
                       "~/Scripts/js/modernizr.min.js",
                       "~/Scripts/js/pace.min.js",
                // "~/Scripts/js/retina.min.js",
                       "~/Scripts/js/jquery.cookies.js",
                       "~/Scripts/js/jquery.validate.min.js",
                       "~/Scripts/jquery.validate.unobtrusive.js",
                       "~/Scripts/alertify.min.js",                       
                       "~/Scripts/DataTable/jquery.dataTables.min.js"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
