using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace WEB.Controllers.Sistema
{
    public class SistemaController : Controller
    {
        public ActionResult Portal()
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return RedirectToAction("Credenciais", "Login");
                }

                // RESGATA ISTA DE MODULOS DE ACESSO
                var bll = new Usuario();
                var usuarioModuloAcessoLista = new UsuarioModuloAcessoLista();
                string CPF = Session["CPF"].ToString();
                usuarioModuloAcessoLista = bll.UsuarioModulos(CPF);

                return View("Portal", usuarioModuloAcessoLista);
            }
            catch (Exception exception)
            {
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Abrir Portal",
                            "Sistema",
                            "GET - Abrir Portal",
                            exception.ToString()
                        );

                    return View("Erro");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    ViewBag.Retorno = "~/Login/Credenciais";
                    return View("Erro");
                }
            }
        }

    }
}