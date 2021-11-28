using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using DTO;
using System.Configuration;

namespace WEB.Metodos
{
    public class Email
    {
        // RESGATA INFORMAÇÃO DO SERVIDOR PARA DEPLOY
        private int porta()
        {
            string servidor = ConfigurationManager.AppSettings["servidor"];

            if (servidor.Equals("local"))
            {
                return 587;
            }
            else
            {
                return 2550;
            }
        }

        public bool Erro(SistemaErro erro)
        {
            try
            {
                // SETANDDO E-MAIL PERFIL RESPONSAVEL
                var bll = new BLL.Sistema();
                var dadosEmail = new SistemaEmail();
                dadosEmail = bll.siatemaEmail(1);
                
                // CONFIGURAÇÃO CORPO DO E-MAIL
                var mailMessage = new MailMessage();
                mailMessage.To.Add(new MailAddress(dadosEmail.Email));
                mailMessage.From = new MailAddress(dadosEmail.Email);
                mailMessage.Subject = "Erro Siatema | n. " + erro.IdErro;
                mailMessage.IsBodyHtml = true;

                mailMessage.Body =
                "<hr/><br/>Usuario: " + erro.Usuario + "<br/><hr/>" +
                "<hr/><br/>Número Erro: " + erro.IdErro + "<br /><hr/>" +
                "<hr/><br/>Procedimento: " + erro.Procedimento + "<br /><hr/>" +
                "<hr/><br/>Controller: " + erro.Controller + "<br /><hr/>" +
                "<hr/><br/>Action: " + erro.Acao + "<br /><hr/>" +
                "<hr/><br/>Mensagem Erro: " + erro.Erro + "<br/><hr/>";

                // CONFIGURAÇÃO PARA ENVIO
                var smtpCliente = new SmtpClient("mail56.redehost.com.br", porta());
                smtpCliente.Credentials = new NetworkCredential(dadosEmail.Email, dadosEmail.Senha);
                smtpCliente.EnableSsl = false;
                smtpCliente.Send(mailMessage);

                // VARIÁVEL DE RETORNO
                bool retorno = true;
                return retorno;
            }
            catch (Exception ex)
            {
                // VARIÁVEL DE RETORNO
                bool retorno = false;
                return retorno;
            }
        }
    }
}