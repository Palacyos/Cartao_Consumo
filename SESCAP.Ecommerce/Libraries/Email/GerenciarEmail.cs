using System;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Libraries.Seguranca;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Libraries.Email
{
    public class GerenciarEmail
    {


        private SmtpClient _smtp;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAcessor;
       

        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration, IHttpContextAccessor httpContextAcessor)
        {
            _smtp = smtp;
            _configuration = configuration;
            _httpContextAcessor = httpContextAcessor;

        }


        public void LinkResetarSenha(CadastroLoginSescAP cadastro, string idCrip)
        {

            /*
             * -> corpo da mensagem
             */
            var request = _httpContextAcessor.HttpContext.Request;

            string url = $"{request.Scheme}://{request.Host}/Home/CriarSenha/{idCrip}";

            string msgBody = string.Format(
                "<h2>Cadastrar nova senha - Sesc Amapá</h2>" +
                "<h3>Caro Cliente, {1}</h3> <br/>" +
                "Clique no link abaixo para cadastrar uma nova senha!  <br/>" +
                "<a href='{0}' target='_blank'>{0}</a>",
                url,
                cadastro.EMAIL
                );

            /*
             * MailMessage -> Construir a mensagem
             */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(cadastro.EMAIL);
            mensagem.Subject = "Sesc Amapá - Recuperação de Senha" + cadastro.EMAIL;
            mensagem.Body = msgBody;
            mensagem.IsBodyHtml = true;

            /*
             * SendMessage
             */
            _smtp.Send(mensagem);
        }

    }
}
