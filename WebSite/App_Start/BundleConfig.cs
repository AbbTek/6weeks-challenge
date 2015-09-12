using System.Web;
using System.Web.Optimization;

namespace WebSite
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-theme.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/superAdmin").Include(
          "~/Scripts/angular.js",
          "~/Scripts/angular-animate.js",
          "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
          "~/Scripts/lodash.js",
          "~/Scripts/libs/angular-google-maps.js",
          "~/Scripts/libs/angular-confirm.js",
          "~/Scripts/ng-file-upload-shim.js",
          "~/Scripts/ng-file-upload.js",
          

          "~/Scripts/6weekschallenge/6weekschallenge.module.js",
          "~/Scripts/6weekschallenge/core/core.module.js",
          "~/Scripts/6weekschallenge/core/dataservice.js",
          "~/Scripts/6weekschallenge/core/utilsService.js",
          "~/Scripts/6weekschallenge/core/uploadService.js",
          "~/Scripts/6weekschallenge/superAdmin/superAdmin.module.js",
          "~/Scripts/6weekschallenge/superAdmin/superAdmin.js"
          )
          );
        }
    }
}
