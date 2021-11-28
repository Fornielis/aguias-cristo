using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_SITE.Controllers
{
    public class EventosController : Controller
    {
        // RESGATA URL QUE FOI ACESSADA PELO USUÁRIO
        private DTO.Home homeInformacoes()
        {
            // RESGATA INFORMAÇÕES BASEADOS NO HOST SOLICITANTE
            var bll = new BLL.Home();
            var homeInfo = new DTO.Home();
            homeInfo = bll.HomeInfomacoes(Request.Url.Host.ToString());
            return homeInfo;
        }

        // CONVERTE MES NUMERO PARA MES EXTENSO
        private string mesExtenso(string mesNumero)
        {
            // CAPTURA NÚMERO DO MÊS EM QUESTÃO
            int mes = Convert.ToInt32(mesNumero);

            // RETORNA MES POR EXTENSO
            switch (mes)
            {
                case 1:
                    return "JANEIRO";
                case 2:
                    return "FEVEREIRO";
                case 3:
                    return "MARÇO";
                case 4:
                    return "ABRIL";
                case 5:
                    return "MAIO";
                case 6:
                    return "JUNHO";
                case 7:
                    return "JULHO";
                case 8:
                    return "AGOSTO";
                case 9:
                    return "SETEMBRO";
                case 10:
                    return "OUTUBRO";
                case 11:
                    return "NOVEMBRO";
                case 12:
                    return "DEZEMBRO";
                default:
                    return "";
            }
        }

        // CONVERTE MES EXTENSO PARA MES NUMERO
        private string mesNumero(string mesExtenso)
        {
            // RETORNA MES POR NUMERO
            switch (mesExtenso)
            {
                case "JANEIRO":
                    return "01";
                case "FEVEREIRO":
                    return "02";
                case "MARÇO":
                    return "03";
                case "ABRIL":
                    return "04";
                case "MAIO":
                    return "05";
                case "JUNHO":
                    return "06";
                case "JULHO":
                    return "07";
                case "AGOSTO":
                    return "08";
                case "SETEMBRO":
                    return "09";
                case "OUTUBRO":
                    return "10";
                case "NOVEMBRO":
                    return "11";
                case "DEZEMBRO":
                    return "12";
                default:
                    return "";
            }
        }

        // CAPTURA MÊS EM QUESTÃO
        private string mesAtual()
        {
            // CAPTURA NÚMERO DO MÊS EM QUESTÃO
            int mes = DateTime.Now.Month;

            // RETORNA MES POR EXTENSO
            switch (mes)
            {
                case 1:
                    return "JANEIRO";
                case 2:
                    return "FEVEREIRO";
                case 3:
                    return "MARÇO";
                case 4:
                    return "ABRIL";
                case 5:
                    return "MAIO";
                case 6:
                    return "JUNHO";
                case 7:
                    return "JULHO";
                case 8:
                    return "AGOSTO";
                case 9:
                    return "SETEMBRO";
                case 10:
                    return "OUTUBRO";
                case 11:
                    return "NOVEMBRO";
                case 12:
                    return "DEZEMBRO";
                default:
                    return "";
            }
        }

        // CONTROLLERS
        public ActionResult Agenda()
        {

            try
            {
                // INSTÂNCIAS
                var bll = new BLL.Evento();

                // RESGATA INFORMAÇÕES DE ACORDE COM HOST SOLITANTE
                var HomeInfo = homeInformacoes();
                ViewBag.HomeInfo = HomeInfo;

                // RESGATA MÊS ATUAL
                string mes = mesAtual();
                ViewBag.Mes = mes;

                // TRANSFORME MES ATUAL EXTENSO POR NUMERO
                var numeroMes = mesNumero(mes);

                // RESGATA LISTA EVENTOS PARA O MÊS
                var eventoLista = new DTO.EventoLista();
                eventoLista = bll.EventoPorMes(HomeInfo.Unidade, numeroMes);
                ViewBag.EventoLista = eventoLista;

                // RESGATA LISTA DE EVENTOS QUE SERÃO DIVULGADOS NO MÊS EM QUESTÃO
                var eventoListaDivulgacao = new DTO.EventoLista();
                eventoListaDivulgacao = bll.EventoPorMesDivulgacao(HomeInfo.Unidade, mes, numeroMes);
                ViewBag.EventoListaDivulgacao = eventoListaDivulgacao;

                // RESGATA QUANTIDADE DE REGISTROS QUE RETORNARAM PARA LISTA DIVULGAÇÃO
                int numeroRegistro = eventoListaDivulgacao.Count();
                ViewBag.QuantidadeEventoListaDivulgacao = numeroRegistro;

                return View("Agenda");
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB_SITE.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            "Anônimo",
                            "Evento",
                            "Evento",
                            "GET - Evento",
                            exception.ToString()
                        );

                    return PartialView("~/Views/LayoutPadrao/_HomeErro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/LayoutPadrao/_HomeErro.cshtml");
                }
            }
        }
        public PartialViewResult AgendaPorMes(string mesEvento)
        {

            try
            {
                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var eventoLista = new DTO.EventoLista();

                // RESGATA INFORMAÇÕES DE ACORDE COM HOST SOLITANTE
                var HomeInfo = homeInformacoes();
                ViewBag.HomeInfo = HomeInfo;

                // RESGATA LISTA EVENTOS PARA O MÊS
                eventoLista = bll.EventoPorMes(HomeInfo.Unidade, mesEvento);
                ViewBag.EventoLista = eventoLista;

                // TRANSPOSTA MÊS PESQUISADO
                ViewBag.Mes = mesExtenso(mesEvento);

                return PartialView("_Agenda");
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB_SITE.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            "Anônimo",
                            "Evento",
                            "Evento",
                            "GET - Evento Por Mês",
                            exception.ToString()
                        );

                    return PartialView("~/Views/LayoutPadrao/_HomeErro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/LayoutPadrao/_HomeErro.cshtml");
                }
            }
        }
    }
}