using System.Web;
using System.Web.Optimization;

namespace BusinessInteractive.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/toastr.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css", "~/Content/toastr.css"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                        "~/Scripts/application.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/details").Include(
                        "~/Scripts/Detail.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/json2.js", 
                        "~/Scripts/jquery.signalR-{version}.js"));
        }
    }
}