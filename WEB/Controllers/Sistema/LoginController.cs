using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace WEB.Controllers.Sistema
{
    public class LoginController : Controller
    {
        // PARTIAL VIEW
        public ActionResult Credenciais()
        {
            // ABANDONA SESSÕES CRIADAS
            Session.Abandon();

            //MODELO AUTENTICAÇÃO
            var usuarioLogin = new UsuarioLogin();

            return View("Credenciais", usuarioLogin);
        }

        [HttpPost]
        public ActionResult Credenciais(UsuarioLogin usuarioLogin)
        {
            try
            {
                //TESTA SE DADOS ESTÃO VAZIO
                if ((usuarioLogin.Usuario == null || usuarioLogin.Usuario == "") || usuarioLogin.Senha == null || usuarioLogin.Senha == "")
                {
                    ViewBag.Erro = "CamposNull";
                    return View("Credenciais", usuarioLogin);
                }
                else
                {
                    //INSTANCIAS
                    var bll = new Usuario();
                    var usuario = new UsuarioAutenticado();

                    //VERIFICA SE USUÁRIO EXISTE 
                    usuario = bll.usuarioAutenticacao(usuarioLogin);

                    if (usuario.PerfilAcesso != null)
                    {
                        // CRIA SESSÃO NO SERVIDOR
                        Session["Nome"] = usuario.Nome;
                        Session["PerfilAcesso"] = usuario.PerfilAcesso;
                        Session.Timeout = 10;

                        return RedirectToAction("Portal", "Sistema");
                    }

                    //SE USUÁRIO NÃO EXISTE 
                    else
                    {
                        ViewBag.Erro = "LogInnvalido";
                        usuarioLogin = null;
                        return View("Credenciais", usuarioLogin);
                    }
                }             
            }
            catch (Exception exception)
            {
                //LÓGICA PARA GRAAR ERRO NO BANCO
                try
                {
                    // ENVIA ERRO
                    var metodo = new WEB.Metodos.Erro();
                    ViewBag.Retorno = metodo.ErroSitema (
                            usuarioLogin.Usuario,
                            "Tentar Logar",
                            "Login",
                            "POST - Credenciais",
                            exception.ToString()
                        );

                    return View("Erro");
                }

                //CASO NÃO SEJA POSSIVEL GRAVAR O ERRO
                catch (Exception e)
                {
                    return View("Erro");
                }
            }
        }

    }
}