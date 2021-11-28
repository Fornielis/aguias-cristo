using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_SITE.Controllers
{
    public class HomeController : Controller
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

        // CONTROLLERS
        public ActionResult Index()
        {
            try
            {
                // RESGATA INFORMAÇÕES DE ACORDE COM HOST SOLITANTE
                ViewBag.HomeInfo = homeInformacoes();

                return View("Index");
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
                            "Home",
                            "Home",
                            "GET - Home",
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
        public PartialViewResult Contato()
        {
            try
            {
                // INSTANCIAS
                var contato = new DTO.Contato();

                // RESGATA INFORMAÇÕES DE ACORDE COM HOST SOLITANTE
                ViewBag.HomeInfo = homeInformacoes();

                return PartialView("~/Views/Home/_Contato.cshtml");
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

                // RESGATA INFORMAÇÕES DE ACORDE COM HOST SOLITANTE
                var homeInfo = homeInformacoes();

                // ENVIAR E-MAIL
                var metodo = new WEB_SITE.Metodos.Email();
                bool retorno = metodo.ContatoRetorno(contato, homeInfo.IdEmail, homeInfo.Unidade);
                bool providencias = metodo.ContatoProvidencias(contato, homeInfo.IdEmail, homeInfo.Unidade);

                // VIEW BAG
                ViewBag.Nome = contato.Nome.ToUpper();
                ViewBag.Retorno = "true";
                ViewBag.HomeInfo = homeInformacoes();

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
                    var metodo = new WEB_SITE.Metodos.Erro();
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
        public PartialViewResult ListarBannerAtivo()
        {
            try
            {
                // RESGATA INFORMAÇÕES DE ACORDE COM HOST SOLITANTE
                var homeInfo = homeInformacoes();

                // RESGATA IMAGENS BANNERS ATIVOS
                var bll = new BLL.Banner();
                var bannerLista = new DTO.BannerLista();
                bannerLista = bll.ImagensBannerAtivo(homeInfo.Unidade);

                return PartialView("~/Views/Home/_Carrossel.cshtml", bannerLista);
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
                            "Listar Banner Ativo",
                            "Home",
                            "GET - Listar Banner Ativo",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Home/_CarrosselErro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Home/_CarrosselErro.cshtml");
                }
            }
        }
        public PartialViewResult ListarUnidadeAtiva()
        {
            try
            {
                // INSTANCIAS
                var bll = new BLL.Unidade();
                var regional = new DTO.UnidadeLista();
                var estados = new DTO.UnidadeLista();
                var paises = new DTO.UnidadeLista();

                // PREENCHIMENTOS ENVIO POR VIEW.BAGS
                ViewBag.Regional = regional = bll.UnidadePorRegional("SÂO PAULO");
                ViewBag.Estado = estados = bll.UnidadePorEstado();
                ViewBag.Paises = paises = bll.UnidadeOutrosPaises();

                // VERIFICA SE EXSTEM ITENS A SEREM EXIBIDOS
                if (regional.Count() == 0 && estados.Count() == 0 && paises.Count() == 0)
                {
                    return PartialView("~/Views/Home/_UnidadeErro.cshtml");

                }
                else
                {
                    return PartialView("~/Views/Home/_Unidade.cshtml");
                }
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
                            "Listar Unidade Ativa",
                            "Home",
                            "GET - Listar Unidade Ativa",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Home/_UnidadeErro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Home/_UnidadeErro.cshtml");
                }
            }
        }
        public PartialViewResult DetalheUnidade(int idUnidade)
        {
            try
            {
                //RESGATA DADOS UNIDADES
                var unidade = new DTO.Unidade();
                var bll = new BLL.Unidade();
                unidade = bll.UnidadePorId(idUnidade);

                return PartialView("~/Views/Home/_UnidadeDetalhe.cshtml", unidade);
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB_SITE.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Detalhe Unidade",
                            "Home",
                            "GET - Detalhe Unidade",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Home/_UnidadeErro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Home/_UnidadeErro.cshtml");
                }
            }
        }
        public PartialViewResult PorRegiaoUnidade(string uf)
        {
            try
            {
                //RESGATA DADOS UNIDADES
                var unidades = new DTO.UnidadeLista();
                var bll = new BLL.Unidade();
                unidades = bll.UnidadeEstado(uf);

                // VERIFICA SE EXISTE MAIS DE UMA UNIDADE
                if (unidades.Count() > 1)
                {
                    ViewBag.Item = uf;
                    return PartialView("~/Views/Home/_UnidadePorRegiao.cshtml", unidades);
                }
                else
                {
                    // RECUPERA ID UNIDADE
                    int idUnidade = unidades[0].IdUnidade;

                    //RESGATA DADOS UNIDADES
                    var unidade = new DTO.Unidade();
                    unidade = bll.UnidadePorId(idUnidade);

                    return PartialView("~/Views/Home/_UnidadeDetalhe.cshtml", unidade);
                }
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB_SITE.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Unidades por Região",
                            "Home",
                            "GET - Unidade por Região",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Home/_UnidadeErro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Home/_UnidadeErro.cshtml");
                }
            }
        }
        public PartialViewResult PorPaisUnidade(string pais)
        {
            try
            {
                //RESGATA DADOS UNIDADES
                var unidades = new DTO.UnidadeLista();
                var bll = new BLL.Unidade();
                unidades = bll.UnidadePais(pais);

                // VERIFICA SE EXISTE MAIS DE UMA UNIDADE
                if (unidades.Count() > 1)
                {
                    // VIEW BAG RETORNO
                    ViewBag.Item = pais;
                    return PartialView("~/Views/Home/_UnidadePorRegiao.cshtml", unidades);
                }
                else
                {
                    // RECUPERA ID UNIDADE
                    int idUnidade = unidades[0].IdUnidade;

                    //RESGATA DADOS UNIDADES
                    var unidade = new DTO.Unidade();
                    unidade = bll.UnidadePorId(idUnidade);

                    return PartialView("~/Views/Home/_UnidadeDetalhe.cshtml", unidade);
                }
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB_SITE.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Unidades por Pais",
                            "Home",
                            "GET - Unidade por Pais",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Home/_UnidadeErro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Home/_UnidadeErro.cshtml");
                }
            }
        }
    }
}