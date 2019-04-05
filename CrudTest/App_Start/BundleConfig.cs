using System.Web;
using System.Web.Optimization;

namespace CrudTest.UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-3.3.1.min.js")
                .Include("~/Scripts/jquery.mask.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                      "~/Scripts/materialize.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/site").Include(
                     "~/Scripts/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/materialize.min.css",
                      "~/Content/site.css"));
        }
    }
}
