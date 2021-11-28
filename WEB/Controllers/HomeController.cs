using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // ABANDONA SESSÕES CRIADAS
            Session.Abandon();
            return View("Index");
        }
        public PartialViewResult Contato()
        {
            try
            {
                // INSTANCIAS
                var contato = new DTO.Contato();

                return PartialView("~/Views/Home/_Contato.cshtml", contato);
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            "Anônimo",
                            "Contato",
                            "Home",
                            "GET - Contato",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Home/_ContatoErro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Home/_ContatoErro.cshtml");
                }
            }
        }
        public PartialViewResult PostContato(DTO.Contato contato)
        {
            try
            {
                // SALVAR CONTATO NO BANCO
                var bll = new BLL.Home();
                bll.ContatoGravar(contato);

                // ENVIAR E-MAIL
                var metodo = new WEB.Metodos.Email();
                bool retorno = metodo.ContatoRetorno(contato);
                bool providencias = metodo.ContatoProvidencias(contato);

                // VIEW BAG
                ViewBag.Nome = contato.Nome.ToUpper();
                ViewBag.Retorno = "true";

                // RESSETA CONTATO
                contato = null;

                return PartialView("~/Views/Home/_Contato.cshtml", contato);
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            "Anônimo",
                            "Contato",
                            "Home",
                            "POST - Contato",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Home/_ContatoErro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Home/_ContatoErro.cshtml");
                }
            }
        }
    }
}