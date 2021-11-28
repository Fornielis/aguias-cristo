using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using DTO;
using System.Configuration;

namespace WEB_SITE.Metodos
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

        // DEFINE ASSINATURA 
        private string urlAssinatura(string unidade)
        {
            // RETORNA MES POR EXTENSO
            switch (unidade)
            {
                case "SÃO PAULO":                    
                    return "<img src='https://www.mcaguiassp.com.br/public/img/assinaturaSaoPaulo-300x100.jpg'/>";
                case "CAIEIRAS":
                    return "<img src='https://www.mcaguiassp.com.br/public/img/assinaturaCaieiras-300x100.jpg'/>";
                default:
                    return "";
            }
        }

        // MÉTODOS
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
        public bool ContatoRetorno(DTO.Contato contato, int idEmail, string unidade)
        {
            try
            {
                // SETANDDO E-MAIL PERFIL RESPONSAVEL
                var bll = new BLL.Sistema();
                var dadosEmail = new SistemaEmail();
                dadosEmail = bll.siatemaEmail(idEmail);

                // DEFINE URL IMAGEM ASSINATURA
                string UrlAssinatura = urlAssinatura(unidade);

                // CONFIGURAÇÃO CORPO DO E-MAIL
                var mailMessage = new MailMessage();
                mailMessage.To.Add(new MailAddress(contato.Email));
                mailMessage.From = new MailAddress(dadosEmail.Email, "Águias de Cristo - Regional São Paulo");
                mailMessage.Subject = "Confirmação Contato";
                mailMessage.IsBodyHtml = true;

                mailMessage.Body = 
                "Olá " + contato.Nome + ",<br />" +
                "Recebemos sua mensagem, em breve entraremos em contato. <br />" +
                "Que Deus o abençoe! <br /><br />" +
                UrlAssinatura;

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
        public bool ContatoProvidencias(DTO.Contato contato, int idEmail, string unidade)
        {
            try
            {
                // SETANDDO E-MAIL PERFIL RESPONSAVEL
                var bll = new BLL.Sistema();
                var dadosEmail = new SistemaEmail();
                dadosEmail = bll.siatemaEmail(idEmail);

                // DEFINE URL IMAGEM ASSINATURA
                string UrlAssinatura = urlAssinatura(unidade);

                // CONFIGURAÇÃO CORPO DO E-MAIL
                var mailMessage = new MailMessage();
                mailMessage.To.Add(new MailAddress(dadosEmail.Email));
                mailMessage.From = new MailAddress(dadosEmail.Email, "Contato via SIte");
                mailMessage.Subject = "Por " + contato.Nome;
                mailMessage.IsBodyHtml = true;

                mailMessage.Body =
                "<hr/><br/>Nome: " + contato.Nome + "<br/><hr/>" +
                "<hr/><br/>E-mail: " + contato.Email + "<br /><hr/>" +
                "<hr/><br/>Mensagem: " + contato.Mensagem + "<br /><hr/>" +
                UrlAssinatura;

                // CONFIGURAÇÃO PARA ENVIO
                var smtpCliente = new SmtpClient("mail56.redehost.com.br", porta());
                smtpCliente.Credentials = new NetworkCredential(dadosEmail.Email, dadosEmail.Senha);
                smtpCliente.EnableSsl = false;
                smtpCliente.Send(mailMessage);

                // VARIÁVEL DE RETORNO
                bool retorno = true;
                return retorno;
            }
            catch (Exception)
            {
                // VARIÁVEL DE RETORNO
                bool retorno = false;
                return retorno;
            }
        }
    }
}