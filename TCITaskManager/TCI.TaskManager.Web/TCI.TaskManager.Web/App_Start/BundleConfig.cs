using System.Web;
using System.Web.Optimization;

namespace TCI.TaskManager.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/all.css")
                 .Include("~/font-awesome/css/font-awesome.css")
                 .Include("~/css/bootstrap.css")
                 .Include("~/css/sb-admin.css")
                 .Include("~/css/layout.css")
                 .Include("~/css/ui-grid.css")
                 .Include("~/css/kendo.common.min.css")
                 .Include("~/css/kendo.default.min.css")
                 );

            bundles.Add(new ScriptBundle("~/Scripts/all.js")
                .Include("~/Scripts/jquery-1.10.2.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/ui-bootstrap.js")
                .Include("~/Scripts/kendo.all.min.js")
                .Include("~/Scripts/kendo.angular.min.js")
                .Include("~/Scripts/app/app.js")
                .IncludeDirectory("~/Scripts/app/", "*.js", true)
                );
        }
    }
}
