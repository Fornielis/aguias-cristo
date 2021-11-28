using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;

namespace WEB.Controllers
{
    public class BannerController : Controller
    {
        // MÉTODOS
        private UnidadeRegionalLista unidadeRegionalLista()
        {
            // RESGATA LISTA UNIDADES DA REGIONAL
            var bll = new BLL.Unidade();
            var unidadesRegional = new DTO.UnidadeRegionalLista();
            unidadesRegional = bll.UnidadesRegional();
            return unidadesRegional;
        }

        // PARTIAL VIEW
        public PartialViewResult GravarBanner()
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //INSTÂNCIA MODELO
                var banner = new DTO.Banner();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                return PartialView("_GravarBanner", banner);
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Gravar Banner",
                            "Banner",
                            "GET - Gravar Banner",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }
            }
        }
        public PartialViewResult PostGravarBanner(DTO.Banner banner)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // CONVERTE BASE64 PARA BYTES
                var metodo = new WEB.Metodos.Imagens();
                banner.ImagemByte = metodo.byteImage(banner.base64imagem);
                banner.base64imagem = null;

                // INSTÂNCIAS
                var bll = new BLL.Banner();
                var bannerLista = new DTO.BannerLista();

                // SALVA IMAGEM NO BANCO
                int Idretorno = bll.BannerGravar(banner);

                //VIEW BAG PARA ABERTURA DE RETORNO
                ViewBag.Retorno = "GRAVAR";

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarBanner", bannerLista);
                }
                // SENDO USUÁRIO
                else
                {
                    var unidade = Session["Unidade"].ToString();
                    bannerLista = bll.bannerListarPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (bannerLista.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarBanner", bannerLista);
                }
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Gravar Banner",
                            "Banner",
                            "POST - Gravar Banner",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }
            }
        }
        public PartialViewResult ListarBanner()
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Banner();
                var bannerLista = new DTO.BannerLista();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarBanner", bannerLista);
                }
                // SENDO USUÁRIO
                else
                {
                    var unidade = Session["Unidade"].ToString();
                    bannerLista = bll.bannerListarPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (bannerLista.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarBanner", bannerLista);
                }

            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Listar Banner",
                            "Banner",
                            "GET - Listar Banner",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }
            }
        }
        public PartialViewResult ListarBannerPorUnidade(string unidade)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // RESGATA LISTA DE BANNER POR UNIDADE
                var bll = new BLL.Banner();
                var bannerLista = new DTO.BannerLista();
                bannerLista = bll.bannerListarPorUnidade(unidade);

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // RETORNO UNIDADE PESQUISADA
                ViewBag.UnidadePesquisada = unidade;

                // VARIAVEL HABILITA BARRA PESQISA
                if (bannerLista.Count() > 0)
                {
                    ViewBag.Pesquisa = true;
                }

                // TRANPORTA UNIDADE A SER PESQUISADA
                ViewBag.Unidade = unidade;

                return PartialView("_ListarBanner", bannerLista);

            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Listar Banner Por Unidade",
                            "Banner",
                            "GET - Listar Banner Por Unidade",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }
            }
        }
        public PartialViewResult AlterarBanner(int idBanner)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // RESGATA LISTA BANNERS
                var bll = new BLL.Banner();
                var banner = new DTO.Banner();
                banner = bll.ListarBannerPorId(idBanner);

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                return PartialView("_AlterarBanner", banner);
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Alterar Banner",
                            "Banner",
                            "GET - Alterar Banner",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }
            }
        }
        public PartialViewResult PostAlterarBanner(DTO.Banner banner)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // CONVERTE BASE64 PARA BYTES
                var metodo = new WEB.Metodos.Imagens();
                banner.ImagemByte = metodo.byteImage(banner.base64imagem);
                banner.base64imagem = null;

                // INSTÂNCIAS
                var bll = new BLL.Banner();
                var bannerLista = new DTO.BannerLista();

                // ALTERA BANNER
                bll.AlterarBanner(banner);

                //VIEW BAG PARA ABERTURA DE RETORNO
                ViewBag.Retorno = "ALTERAR";

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarBanner", bannerLista);
                }
                // SENDO USUÁRIO
                else
                {
                    var unidade = Session["Unidade"].ToString();
                    bannerLista = bll.bannerListarPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (bannerLista.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarBanner", bannerLista);
                }

            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Alterar Banner",
                            "Banner",
                            "POST - Alterar Banner",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }
            }
        }
        public PartialViewResult DeletarBanner(int idBanner)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Banner();
                var bannerLista = new DTO.BannerLista();

                // DELETA BANNER
                bll.DeletarBanner(idBanner);

                // VIEW BAG RETORNO
                ViewBag.Retorno = "DELETAR";

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarBanner", bannerLista);
                }
                // SENDO USUÁRIO
                else
                {
                    var unidade = Session["Unidade"].ToString();
                    bannerLista = bll.bannerListarPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (bannerLista.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarBanner", bannerLista);
                }
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Deletar Banner",
                            "Banner",
                            "POST - Deletar Banner",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }
            }
        }
        public PartialViewResult PostPesquisarBanner(FormCollection valorInformado)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }
                // INSTANCIA BLL
                var bll = new BLL.Banner();

                // RESGATA LISTA BANNERS
                var bannerLista = new DTO.BannerLista();
                string valorPesquisa = valorInformado["valorInformado"];
                string unidadePesquisa = valorInformado["unidade"];
                bannerLista = bll.PequisarBanner(valorPesquisa, unidadePesquisa);

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();


                // RETORNO UNIDADE PESQUISADA
                ViewBag.UnidadePesquisada = unidadePesquisa;

                // VARIAVEL HABILITA BARRA PESQISA
                ViewBag.Pesquisa = true;

                // TRANPORTA UNIDADE A SER PESQUISADA
                ViewBag.Unidade = unidadePesquisa;

                return PartialView("_ListarBanner", bannerLista);
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema(
                            Session["Nome"].ToString(),
                            "Pesquisar Banner",
                            "Banner",
                            "POST - Pesquisar Banner",
                            exception.ToString()
                        );

                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return PartialView("~/Views/Sistema/_Erro.cshtml");
                }
            }
        }
    }
}