using System.Web;
using System.Web.Optimization;

namespace WEB_SITE
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // INDEX ///////////////////////////////////////////////////////////
            // CSS - HOME
            bundles.Add(new StyleBundle("~/Content/Index").Include(
                      "~/Content/Global/bootstrap.css",
                      "~/Content/Global/cores.css",
                      "~/Content/Animacoes/animate.css",
                      "~/Content/LayoutPadrao/_LayoutHome.css",
                      "~/Content/Home/index.css"));
            // JS -HOME
            bundles.Add(new ScriptBundle("~/bundles/Index").Include(
                      "~/Scripts/Global/jquery-{version}.js",
                      "~/Scripts/Global/jquery.unobtrusive-ajax.js",
                      "~/Scripts/Validacao/jquery.validate*",
                      "~/Scripts/Global/bootstrap.js",
                      "~/Scripts/LayoutPadrao/_LayoutHome.js",
                      "~/Scripts/Home/index.js"));
            // EVENTO ///////////////////////////////////////////////////////////
            // JS -EVENTO
            bundles.Add(new ScriptBundle("~/bundles/Evento").Include(
                      "~/Scripts/Global/jquery-{version}.js",
                      "~/Scripts/Global/jquery.unobtrusive-ajax.js",
                      "~/Scripts/Global/bootstrap.js",
                      "~/Scripts/LayoutPadrao/_LayoutHome.js",
                      "~/Scripts/Home/evento.js"));
            
            //=================================================================//
            // CSS - ERRO
            bundles.Add(new StyleBundle("~/Content/erro").Include(
                      "~/Content/Global/bootstrap.css",
                      "~/Content/Animacoes/animate.css",
                      "~/Content/Global/cores.css",
                      "~/Content/Sistema/erro.css"));
            // JS - ERRO
            bundles.Add(new ScriptBundle("~/bundles/erro").Include(
                      "~/Scripts/Global/jquery-{version}.js",
                      "~/Scripts/Global/jquery.unobtrusive-ajax.js",
                      "~/Scripts/Global/bootstrap.js",
                      "~/Scripts/Sistema/erro.js"));
        }
    }
}
