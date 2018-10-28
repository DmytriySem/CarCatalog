using System.Web;
using System.Web.Optimization;

namespace CarCatalog
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jqgrid").Include(
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/jquery.jqGrid/ui.jqgrid.css"));

            //---------------------------------------------------------------------------------
            bundles.Add(new StyleBundle("~/Content/Tile").Include(
                "~/Content/CSS/Catalog.css",
                "~/Content/jsTree/themes/default/style.min.css",
                "~/Content/jsTree/themes/default-dark/style.min.css"
                ));

            //---------------------------------------------------------------------------------
            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                "~/Scripts/jquery.jqGrid.min.js",
                "~/Scripts/jquery-ui-1.10.0.min.js",
                "~/Scripts/i18n/grid.locale-en.js"));

            bundles.Add(new ScriptBundle("~/bundles/Tile").Include(
                "~/Scripts/Script/tile.js"));

            //---------------------------------------------------------------------------------
            bundles.Add(new ScriptBundle("~/bundles/jsTree").Include(
                "~/Scripts/jsTree3/jstree.min.js"));

            //---------------------------------------------------------------------------------
            bundles.Add(new ScriptBundle("~/bundles/grid").Include(
                "~/Scripts/Script/Grid.js"));

            //---------------------------------------------------------------------------------
            bundles.Add(new ScriptBundle("~/bundles/brandDialog").Include(
                "~/Scripts/Script/BrandDialog.js"));

            bundles.Add(new ScriptBundle("~/bundles/modalDialog").Include(
                "~/Scripts/Script/ModalDialog.js"));

        }
    }
}
