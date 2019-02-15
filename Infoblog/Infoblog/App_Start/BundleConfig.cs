using System.Web;
using System.Web.Optimization;

namespace Infoblog
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
       /*     bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/moment.js",
                "~/Scripts/fullcalendar*",
                "~/Scripts/qTip/jquery.qtip.js"));
                */
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                "~/Scripts/jquery.unobtrusive*",
                                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/moment.js",
                "~/Scripts/fullcalendar*",
                "~/Scripts/locale/sv.js",
                "~/Scripts/qTip/jquery.qtip.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-journal.css", 
                      "~/Content/VoteStyle.css",
                      "~/Content/site.css",
                      "~/Scripts/qTip/jquery.qtip.css",
                      "~/Content/fullcalendar.css"));
        }
    }
}
