using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace WEB.Controllers
{
    public class UnidadeController : Controller
    {
        // MÉTOOS
        public DTO.UnidadeLista unidadeLista()
        {
            //RESGATA LISTA DE UNIDADES
            var unidadeLista = new DTO.UnidadeLista();
            var bll = new BLL.Unidade();
            unidadeLista = bll.UnidadeListar();
            return unidadeLista;
        }

        // PARTIAL VIEW
        public PartialViewResult GravarUnidade()
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //INSTÂNCIA MODELO
                var unidade = new DTO.Unidade();

                return PartialView("_GravarUnidade", unidade);
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
                            "Gravar Unidade",
                            "Unidade",
                            "GET - Gravar Unidade",
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
        public PartialViewResult PostGravarUnidade(DTO.Unidade unidade)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //GRAVAR UNIDADE
                var bll = new BLL.Unidade();
                bll.GravarUnidade(unidade);

                //RESGATA LISTA DE UNIDADES
                var unidades = unidadeLista();

                //VIEW BAG PARA ABERTURA DE RETORNO
                ViewBag.Retorno = "GRAVAR";

                //RETORNA PARA LISTA UNIDADES
                return PartialView("_ListarUnidade", unidades);
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
                            "Salvar Unidade",
                            "Unidade",
                            "POST - Gravar Unidade",
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
        public PartialViewResult ListarUnidade()
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //RESGATA LISTA DE UNIDADES
                var unidades = unidadeLista();

                return PartialView("_ListarUnidade", unidades);
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
                            "Listar Unidade",
                            "Unidade",
                            "GET - Listar Unidade",
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
        public PartialViewResult PostPesquisarUnidade(FormCollection valorInformado)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //RESGATA LISTA DE UNIDADES
                var unidadeLista = new DTO.UnidadeLista();
                var bll = new BLL.Unidade();
                string valor = valorInformado["valorInformado"];
                unidadeLista = bll.UnidadePesquisa(valor);

                return PartialView("_ListarUnidade", unidadeLista);
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
                            "Pesquisar Unidade",
                            "Unidade",
                            "POST - Pesquisar Unidade",
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
        public PartialViewResult AlterarUnidade(int idUnidade)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //RESGATA DADOS UNIDADES
                var unidade = new DTO.Unidade();
                var bll = new BLL.Unidade();
                unidade = bll.UnidadePorId(idUnidade);

                return PartialView("_AlterarUnidade", unidade);
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
                            "Alterar Unidade",
                            "Unidade",
                            "GET - Alterar Unidade",
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
        public PartialViewResult PostAlterarUnidade(DTO.Unidade unidade)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //ALTERA DADOS
                var bll = new BLL.Unidade();
                bll.UnidadeAlterar(unidade);

                //RESGATA LISTA DE UNIDADES
                var unidades = unidadeLista();

                //VIEW BAG PARA ABERTURA DE RETORNO
                ViewBag.Retorno = "ALTERAR";

                //RETORNA PARA LISTA UNIDADES
                return PartialView("_ListarUnidade", unidades);
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
                            "Alterar Unidade",
                            "Unidade",
                            "POST - Alterar Unidade",
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
        public PartialViewResult DeleatarUnidade(int idUnidade)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //DELETAR UNIDADE
                var bll = new BLL.Unidade();
                bll.UnidadeDeletar(idUnidade);

                //RESGATA LISTA DE UNIDADES
                var unidades = unidadeLista();

                //VIEW BAG PARA ABERTURA DE RETORNO
                ViewBag.Retorno = "DELETAR";

                //RETORNA PARA LISTA UNIDADES
                return PartialView("_ListarUnidade", unidades);
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
                            "Deletar Unidade",
                            "Unidade",
                            "POST - Deletar Unidade",
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