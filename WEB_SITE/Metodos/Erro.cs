using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace WEB_SITE.Metodos
{
    public class Erro
    {       
        public int ErroSitema(string usuario, string proedimento, string controller, string action, string erroAplicacao)
        {
            //INSTÂNCIAS
            var bllSistemas = new BLL.Sistema();
            var metodosEmail = new Metodos.Email();

            //PREENCHIMENTO ERRO
            var erro = new DTO.SistemaErro();
            erro.Usuario = usuario;
            erro.Procedimento = proedimento;
            erro.Controller = controller;
            erro.Acao = action;
            erro.Erro = erroAplicacao;

            //GRAVA ERRO NO BANCO | RESGATA ID DO ERRO
            erro.IdErro = bllSistemas.GravarErro(erro);

            //ENVIA E-MAIL PARA ADMINISTRADOR DO SISTEMA
            bool emailErro = metodosEmail.Erro(erro);

            return erro.IdErro;
        }
    }
}