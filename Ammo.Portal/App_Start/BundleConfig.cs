using System.Web;
using System.Web.Optimization;

namespace Ammo.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/API/jquery-1.11.3.min.js",
                        "~/Scripts/API/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/API/modernizr.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                        "~/Scripts/API/angular.min.js",
                        "~/Scripts/Controllers/AMMO.Base.js",
                        "~/Scripts/Controllers/AMMO.Modal.js",
                        "~/Scripts/Controllers/AMMO.Form.js"));

           bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/API/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/ammo-journal").Include(
                        "~/Scripts/Controllers/AMMO.Journal.js",
                        "~/Scripts/Controllers/AMMO.Menu.js"));

            bundles.Add(new ScriptBundle("~/bundles/landing").Include(
                      "~/Scripts/API/jquery-{version}.min.js",
                      "~/Scripts/Controllers/AMMO.Landing.js",
                      "~/Scripts/API/modernizr.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/API/bootstrap.min.js",
                      "~/Scripts/API/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/modal").Include(
                     "~/Scripts/API/classie.js",
                     "~/Scripts/API/modalEffects.js"));

            bundles.Add(new StyleBundle("~/Content/General").Include(
                      "~/Content/AMMO.ThirdParty.css",
                      "~/Content/AMMO.Fonts.css",
                      "~/Content/AMMO.General.css",
                      "~/Content/AMMO.Modal.css",
                      "~/Content/AMMO.Responsive.css"));

            bundles.Add(new StyleBundle("~/Content/Calendar").Include(
                      "~/Content/AMMO.Calendar.css"));

            bundles.Add(new StyleBundle("~/Content/Journal").Include(
                      "~/Content/AMMO.Menu.css",
                      "~/Content/AMMO.Journal.css",
                      "~/Content/AMMO.Bullet.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
