using System.Web;
using System.Web.Optimization;

namespace APP.Store.Mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/scripts/bootstrap.js",
                      "~/Content/scripts/respond.js")); 
            
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Content/plugins/jQueryUi/jquery-ui.js",
                        "~/Content/scripts/app.min.js",
                        "~/Content/scripts/app-storemanager.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymoney").Include(
                        "~/Content/plugins/maskMoney/jquery.maskMoney.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                       "~/Content/plugins/datepicker/bootstrap-datepicker.js",
                       "~/Content/plugins/datepicker/locales/bootstrap-datepicker.pt-BR.js"));

            bundles.Add(new ScriptBundle("~/bundles/icheck").Include(
                   "~/Content/plugins/iCheck/icheck.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/AdminLTE.css",
                      "~/Content/css/skins/skin-blue.min.css",
                      "~/Content/plugins/datepicker/locales/datepicker3.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                     "~/Content/plugins/datepicker/datepicker3.css"));

            bundles.Add(new StyleBundle("~/Content/icheck").Include(
                    "~/Content/plugins/iCheck/square/blue.css"));


            BundleTable.EnableOptimizations = false;
        }
    }
}
