using System.Web;
using System.Web.Optimization;

namespace CloudClinic
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.3.js"));
            

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include(
                        "~/Scripts/jquery.unobtrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
            "~/Scripts/kendo/2014.3.1411/kendo.all.min.js",
            // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            "~/Scripts/kendo/2014.3.1411/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/2014.3.1411/css").Include(
                        "~/Content/kendo/2014.3.1411/kendo.common-bootstrap.min.css",
                        "~/Content/kendo/2014.3.1411/kendo.bootstrap.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/form").Include(
            "~/Scripts/common/form.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetime").Include(
            "~/Scripts/moment*",
            "~/Scripts/bootstrap-datetimepicker*",
            "~/Scripts/common/datetimepicker-init.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/datetime").Include(
                      "~/Content/bootstrap-datetimepicker*"));
        }
    }
}
