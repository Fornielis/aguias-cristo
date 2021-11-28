using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class EventoController : Controller
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
        public PartialViewResult GravarEvento()
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //INSTÂNCIA MODELO
                var evento = new DTO.Evento();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                return PartialView("_GravarEvento", evento);
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
                            "Gravar Evento",
                            "Evento",
                            "GET - Gravar Evento",
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
        public PartialViewResult PostGravarEvento(DTO.Evento evento)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                var bll = new BLL.Evento();
                int Idretorno = bll.EventoGravar(evento);

                // VARIÁVEL COM ID DO EVENTO SALVO
                ViewBag.IdEvento = Idretorno;

                return PartialView("_GravarDatas", evento);
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
                            "Gravar Evento",
                            "Evento",
                            "POST - Gravar Evento",
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
        public PartialViewResult PostGravarEventoData(DTO.EventoData eventoData)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var datas = new DTO.EventoDataLista();
                var data = new DTO.EventoData();

                // VERIFICA SE DATA EXISTE E SE É RESERVADA NO BANCO
                data = bll.EventoDataReservadaExistente(eventoData.DataEvento);
                if (data.IdData > 0)
                {
                    ViewBag.IdEvento = eventoData.IdEvento;
                    return PartialView("_RetornoDataExistente");
                }

                // VERIFICA SE CASO QUEIRA RESERVA SE A DATA EXISTE NO BANCO
                data = bll.EventoDataExistente(eventoData.DataEvento);
                if (data.IdData > 0 && eventoData.Reservado == "SIM")
                {
                    ViewBag.IdEvento = eventoData.IdEvento;
                    return PartialView("_RetornoDataExistente");
                }
                else
                {
                    // SALVA DATA NO BANCO
                    bll.EventoDataGravar(eventoData);

                    // RESGATA LISTA DE DATAS
                    ViewBag.Datas = bll.EventoDatasPorEvento(eventoData.IdEvento);

                    return PartialView("_ListaDatas");
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
                            "Gravar Datas Evento",
                            "Evento",
                            "POST - Gravar Datas Evento",
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
        public PartialViewResult EventoDatasPorEvento(int IdEvento)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var datas = new DTO.EventoDataLista();

                // RESGATA LISTA DE DATAS
                datas = bll.EventoDatasPorEvento(IdEvento);

                return PartialView("_ListaDatas", datas);
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
                            "Listar Datas Evento",
                            "Evento",
                            "Listar Datas Evento",
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
        public PartialViewResult DeletarEventoData(int IdData, int IdEvento)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var datas = new DTO.EventoDataLista();
                
                // DELETA DATA 
                bll.EventoDataDeletar(IdData, IdEvento);

                // RESGATA LISTA DE DATAS
                ViewBag.Datas = bll.EventoDatasPorEvento(IdEvento);

                return PartialView("_ListaDatas");
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
                            "Deletar Datas Evento",
                            "Evento",
                            "Deletar Datas Evento",
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
        public PartialViewResult ListarEventoData(int IdEvento)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // RESGATA LISTA DE DATAS
                var bll = new BLL.Evento();
                var datas = new DTO.EventoDataLista();
                ViewBag.Datas = bll.EventoDatasPorEvento(IdEvento);

                return PartialView("_ListaDatas");
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
                            "Listar Datas Evento",
                            "Evento",
                            "GET - Listar Datas Evento",
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
        public PartialViewResult PostGravarMesDivulgacao(DTO.EventosMesDivulgacao eventosMesDivulgacao)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var mesesDivulgação = new DTO.EventosMesDivulgacaoLista();

                // SALVA MÊS NO BANCO
                bll.EventoDataDivulgacaoGravar(eventosMesDivulgacao);

                // RESGATA LISTA DE MESES          
                ViewBag.Meses = bll.EventoMesDivulgacaoPorEvento(eventosMesDivulgacao.IdEvento);

                return PartialView("_ListaMeses");
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
                            "Gravar Mes Divulgação",
                            "Evento",
                            "POST - Gravar Mes Divulgação",
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
        public PartialViewResult DeletarMesDivulgacao(int IdMes, int IdEvento)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var eventosMesDivulgacaoLista = new DTO.EventosMesDivulgacaoLista();

                // DELETA MES DIVULGAÇÃO
                bll.EventoMesDivulgacaoDeletar(IdMes);

                // RESGATA LISTA DE DATAS
                ViewBag.Meses = bll.EventoMesDivulgacaoPorEvento(IdEvento);

                return PartialView("_ListaMeses");
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
                            "Deletar Mes Divulgacao",
                            "Evento",
                            "Deletar Mes Divulgacao",
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
        public PartialViewResult ListarMesDivulgacao(int IdEvento)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // RESGATA LISTA DE DATAS
                var bll = new BLL.Evento();
                var eventosMesDivulgacaoLista = new DTO.EventosMesDivulgacaoLista();
                ViewBag.Meses = bll.EventoMesDivulgacaoPorEvento(IdEvento);

                return PartialView("_ListaMeses");
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
                            "Listar Mes Divulgacao",
                            "Evento",
                            "GET - Listar Mes Divulgacao",
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
        public ActionResult ListarEventos()
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var eventos = new DTO.EventoLista();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarEventos", eventos);
                }
                // SENDO USUÁRIO
                else
                {
                    // RESGATA LISTA MEMBROS
                    var unidade = Session["Unidade"].ToString();
                    eventos = bll.EventoPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (eventos.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANSPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarEventos", eventos);
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
                            "Listar Eventos",
                            "Eventos",
                            "GET - Listar Eventos",
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
        public PartialViewResult ListarEventosPorUnidade(string unidade)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var eventos = new DTO.EventoLista();

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // RESGATA LISTA MEMBROS
                eventos = bll.EventoPorUnidade(unidade);

                // RETORNO UNIDADE PESQUISADA
                ViewBag.UnidadePesquisada = unidade;

                // VARIAVEL HABILITA BARRA PESQISA
                if (eventos.Count() > 0)
                {
                    ViewBag.Pesquisa = true;
                }

                // TRANPOSTA UNIDADE A SER PESQUISADA
                ViewBag.Unidade = unidade;

                return PartialView("_ListarEventos", eventos);
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
                            "Listar Eventos Por Unidade",
                            "Evento",
                            "GET - Listar Evento Por Unidade",
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
        public PartialViewResult PostPesquisarEvento(FormCollection valorInformado)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var eventos = new DTO.EventoLista();

                // RESGATA LISTA DE EVENTOS BASEADO NAS INFORMAÇÕES DA PESQUISA.
                string valorPesquisa = valorInformado["valorInformado"];
                string unidadePesquisa = valorInformado["unidade"];
                eventos = bll.EventoPesquisa(unidadePesquisa, valorPesquisa);

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // VARIAVEL HABILITA BARRA PESQISA
                ViewBag.Pesquisa = true;

                // RETORNO UNIDADE PESQUISADA
                ViewBag.UnidadePesquisada = unidadePesquisa;

                // TRANSPORTA UNIDADE A SER PESQUISADA
                ViewBag.Unidade = unidadePesquisa;

                return PartialView("_ListarEventos", eventos);
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
                            "Perquisar Eventos",
                            "Eventos",
                            "POST - Pesquisar Eventos",
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
        public PartialViewResult AlterarEvento(int IdEvento)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                //INSTÂNCIAS
                var bll = new BLL.Evento();
                var evento = new DTO.Evento();

                // RESGATA DADOS DO EVENTO
                evento = bll.EventoPorId(IdEvento);

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                return PartialView("_AlterarEvento", evento);
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
                            "Alterar Evento",
                            "Evento",
                            "GET - Alterar Evento",
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
        public PartialViewResult PostAlterarEvento(DTO.Evento evento)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var eventos = new DTO.EventoLista();

                // ALTERA DADOS DO EVENTO NO BANCO
                bll.EventoAlterar(evento);

                // VIEW BAG RETORNO
                ViewBag.Retorno = "ALTERAR";

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarEventos", eventos);
                }
                // SENDO USUÁRIO
                else
                {
                    // RESGATA LISTA MEMBROS
                    var unidade = Session["Unidade"].ToString();
                    eventos = bll.EventoPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (eventos.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANSPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarEventos", eventos);
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
                            "Alterar Evento",
                            "Evento",
                            "POST - Alterar Evento",
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
        public PartialViewResult DeleatarEvento(int IdEvento)
        {
            try
            {
                // TESTA SE A SESSÃO ESTA ATIVA
                if (Session["PerfilAcesso"] == null)
                {
                    return PartialView("~/Views/Sistema/_ErroSessao.cshtml");
                }

                // INSTÂNCIAS
                var bll = new BLL.Evento();
                var eventosMesDivulgacaoLista = new DTO.EventosMesDivulgacaoLista();
                var eventos = new DTO.EventoLista();

                // DELETA EVENTO
                bll.EventoDeletar(IdEvento);

                // VIEW BAG RETORNO
                ViewBag.Retorno = "DELETAR";

                // RESGATA LISTA UNIDADES
                ViewBag.Unidades = unidadeRegionalLista();

                // SENDO DIRETORIA OU SUPORTE
                var perfil = Session["PerfilAcesso"].ToString();
                if (perfil == "DIR" || perfil == "SUP")
                {
                    return PartialView("_ListarEventos", eventos);
                }
                // SENDO USUÁRIO
                else
                {
                    // RESGATA LISTA MEMBROS
                    var unidade = Session["Unidade"].ToString();
                    eventos = bll.EventoPorUnidade(unidade);

                    // VARIAVEL HABILITA BARRA PESQISA
                    if (eventos.Count() > 0)
                    {
                        ViewBag.Pesquisa = true;
                    }

                    // TRANSPORTA UNIDADE A SER PESQUISADA
                    ViewBag.Unidade = unidade;

                    return PartialView("_ListarEventos", eventos);
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
                            "Deletar Evento",
                            "Evento",
                            "Deletar Evento",
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