using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class MembrosController : Controller
    {
        // MÉTODOS
        public UnidadeRegionalLista unidadeRegionalLista()
        {
            // RESGATA LISTA UNIDADES DA REGIONAL
            var bll = new BLL.Unidade();
            var unidadesRegional = new DTO.UnidadeRegionalLista();
            unidadesRegional = bll.UnidadesRegional();
            return unidadesRegional;
        }

        // PARTIAL VIEW
        public ActionResult GravarMembro()
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //INSTÂNCIA MODELO
                var membro = new DTO.Membro();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                return PartialView("_GravarMembro", membro);
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
                            "Gravar membro",
                            "Membro",
                            "GET - Gravar Membro",
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
        public ActionResult PostGravarMembro(DTO.Membro membro)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Membro();
                var membros = new DTO.MembroLista();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // VERIFICA SE CPF JÁ EXISTE
                string cpfPesquisado = bll.CpfExistente(membro.CPF);
                if (cpfPesquisado != "")
                {
                    ViewBag.Retorno = "CPF-EXISTE";
                    return PartialView("_GravarMembro", membro);
                }

                // CONVERTE BASE64 PARA BYTES
                var metodo = new WEB.Metodos.Imagens();
                membro.FotoByte = metodo.byteImage(membro.base64imagem);

                //SALVA MEMBRO NO BANCO
                bll.GravarMembro(membro);
                membro = null;

                // VIEW BAG RETORNO DE GRAVAÇÃO
                ViewBag.Retorno = "GRAVAR";

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarMembro", membros);
                }
                // SENDO USUÁRIO
                else
                {
                    var unidade = Session["Unidade"].ToString();
                    membros = bll.MembrosListarPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (membros.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarMembro", membros);
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
                            "Gravar membro",
                            "Membro",
                            "POST - Gravar Membro",
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
        public ActionResult ListarMembro()
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Membro();
                var membros = new DTO.MembroLista();
                
                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarMembro", membros);
                }
                // SENDO USUÁRIO
                else
                {
                    // RESGATA LISTA MEMBROS
                    var unidade = Session["Unidade"].ToString();
                    membros = bll.MembrosListarPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (membros.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarMembro", membros);
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
                            "Listar membros",
                            "Membro",
                            "GET - Listar Membros",
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
        public ActionResult AlterarMembro(string CPF)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // RESGATA MEMBRO PELO CPF
                var bll = new BLL.Membro();
                var membro = new DTO.Membro();
                membro = bll.MembroPorCpf(CPF);

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                return PartialView("_AlterarMembro", membro);
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
                            "Alterar Membro",
                            "Membro",
                            "GET - Alterar Membro",
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
        public ActionResult PostAlterarMembro(DTO.Membro membro)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Membro();
                var membros = new DTO.MembroLista();

                // VERIFICA SE HOUVE ALTERAÇÃO NO CPF
                // CASO SIM VERIFICA SE O NOVO NÚMERO FORNECIDO EXISTE NO BANCO
                if (membro.CPF != membro.CPFbanco)
                {
                    // SE NOVO CPF INFORMADO JÁ EXISTIR NO BANCO
                    string cpfPesquisado = bll.CpfExistente(membro.CPF);
                    if (cpfPesquisado != "")
                    {
                        ViewBag.Retorno = "CPF-EXISTE";
                        membro.CPF = membro.CPFbanco;
                        return PartialView("_AlterarMembro", membro);
                    }
                }             

                // CONVERTE BASE64 PARA BYTES
                var metodo = new WEB.Metodos.Imagens();
                membro.FotoByte = metodo.byteImage(membro.base64imagem);

                //ALTERA MEMBRO NO BANCO
                bll.AlterarMembro(membro);
                membro = null;

                // VIEW BAG RETORNO DE GRAVAÇÃO
                ViewBag.Retorno = "ALTERAR";

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarMembro", membros);
                }
                // SENDO USUÁRIO
                else
                {
                    var unidade = Session["Unidade"].ToString();
                    membros = bll.MembrosListarPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (membros.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarMembro", membros);
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
                            "Alterar membro",
                            "Membro",
                            "POST - Alterar Membro",
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
        public ActionResult PostPesquisarMembro(FormCollection valorInformado)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Membro();
                var membros = new DTO.MembroLista();

                // RESGATA LISTA DE MEMBROS BASEADO NAS INFORMAÇÕES DA PESQUISA.
                string valorPesquisa= valorInformado["valorInformado"];
                string unidadePesquisa = valorInformado["unidade"];
                membros = bll.MembroPesquisa(valorPesquisa, unidadePesquisa);

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // RETORNO UNIDADE PESQUISADA
                ViewBag.UnidadePesquisada = unidadePesquisa;

                // VARIAVEL HABILITA BARRA PESQISA
                ViewBag.Pesquisa = true;

                // TRANPORTA UNIDADE A SER PESQUISADA
                ViewBag.Unidade = unidadePesquisa;

                return PartialView("_ListarMembro", membros);
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
                            "Pesquisar Membros",
                            "Membros",
                            "POST - Pesquisar Membros",
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
        public ActionResult ListarMembroPorUnidade(string unidade)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Membro();
                var membros = new DTO.MembroLista();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // RESGATA LISTA MEMBROS
                membros = bll.MembrosListarPorUnidade(unidade);

                // RETORNO UNIDADE PESQUISADA
                ViewBag.UnidadePesquisada = unidade;

                // VARIAVEL HABILITA BARRA PESQISA
                if (membros.Count() > 0)
                {
                    ViewBag.Pesquisa = true;
                }

                // TRANPOSTA UNIDADE A SER PESQUISADA
                ViewBag.Unidade = unidade;

                return PartialView("_ListarMembro", membros);
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
                            "Listar Membros Por Unidade",
                            "Membro",
                            "GET - Listar Membros Por Unidade",
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