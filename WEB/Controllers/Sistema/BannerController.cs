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

                // INSTANCIA BLL
                var bll = new BLL.Banner();

                // SALVA IMAGEM NO BANCO
                int Idretorno = bll.BannerGravar(banner);

                // RESGATA LISTA BANNERS
                var bannerLista = new DTO.BannerLista();
                bannerLista = bll.ListarBanner();

                //VIEW BAG PARA ABERTURA DE RETORNO
                ViewBag.Retorno = "GRAVAR";

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
        public PartialViewResult ListarBanner(DTO.Banner banner)
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
                var bannerLista = new DTO.BannerLista();
                bannerLista = bll.ListarBanner();

                return PartialView("_ListarBanner",bannerLista);
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

                return PartialView("_AlterarBanner",banner);
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

                // INSTANCIA BLL
                var bll = new BLL.Banner();

                // ALTERA BANNER
                bll.AlterarBanner(banner);

                // RESGATA LISTA BANNERS
                var bannerLista = new DTO.BannerLista();
                bannerLista = bll.ListarBanner();

                //VIEW BAG PARA ABERTURA DE RETORNO
                ViewBag.Retorno = "ALTERAR";

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
                // INSTANCIA BLL
                var bll = new BLL.Banner();

                // DELETA BANNER
                bll.DeletarBanner(idBanner);

                // RESGATA LISTA BANNERS
                var bannerLista = new DTO.BannerLista();
                bannerLista = bll.ListarBanner();

                // VIEW BAG RETORNO
                ViewBag.Retorno = "DELETAR";

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
                string valor = valorInformado["valorInformado"];
                bannerLista = bll.PequisarBanner(valor);

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