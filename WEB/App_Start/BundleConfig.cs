using System.Web;
using System.Web.Optimization;

namespace WEB
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // SISTEMA /////////////////////////////////////////////////////////
            // CSS - LOGIN
            bundles.Add(new StyleBundle("~/Content/loginInicio").Include(
                      "~/Content/Global/bootstrap.css",
                      "~/Content/Animacoes/animate.css",
                      "~/Content/Global/cores.css",
                      "~/Content/Sistema/login.css",
                      "~/Content/Sistema/erro.css"));
            // JS - LOGIN
            bundles.Add(new ScriptBundle("~/bundles/loginInicio").Include(
                      "~/Scripts/Global/jquery-{version}.js",
                      "~/Scripts/Global/jquery.unobtrusive-ajax.js",
                      "~/Scripts/Validacao/jquery.validate*",
                      "~/Scripts/Global/bootstrap.js",
                      "~/Scripts/Sistema/login.js",
                      "~/Scripts/Sistema/erro.js",
                      "~/Scripts/Global/jquery.mask.js",
                      "~/Scripts/Sistema/formularios.js"));
            //=================================================================//
            // CSS - PORTAL
            bundles.Add(new StyleBundle("~/Content/Portal").Include(
                      "~/Content/Global/bootstrap.css",
                      "~/Content/Animacoes/animate.css",
                      "~/Content/Global/cores.css",
                      "~/Content/Sistema/portal.css",
                      "~/Content/Sistema/formularios.css",
                      "~/Content/Sistema/erro.css"));
            // JS - PORTAL
            bundles.Add(new ScriptBundle("~/bundles/Portal").Include(
                      "~/Scripts/Global/jquery-{version}.js",
                      "~/Scripts/Global/jquery.mask.js",
                      "~/Scripts/Global/jquery.unobtrusive-ajax.js",
                      "~/Scripts/Validacao/jquery.validate*",
                      "~/Scripts/Global/bootstrap.js",
                      "~/Scripts/Sistema/portal.js",
                      "~/Scripts/Sistema/formularios.js"));
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
