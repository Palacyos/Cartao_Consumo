using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _hostingEnv;

        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration, IHttpContextAccessor httpContextAcessor, IWebHostEnvironment hostingEnv)
        {
            _smtp = smtp;
            _configuration = configuration;
            _httpContextAcessor = httpContextAcessor;
            _hostingEnv = hostingEnv;
        }


        public void LinkResetarSenha(CadastroLoginSescAP cadastro, string idCrip)
        {

            /*
             * -> corpo da mensagem
             */
            var request = _httpContextAcessor.HttpContext.Request;

            string url = $"{request.Scheme}://{request.Host}/home/criar-senha/{idCrip}";

            string msgBody = string.Format(
            "<h2>Cadastrar nova senha - Sesc Amapá</h2>" +
            "<h3>Prezado Cliente, {1}</h3> <br/>" +
            "<h4>Atenção: este link expira em 20 minutos.</h4> <br/>" +
            "Clique no link abaixo para cadastrar uma nova senha!  <br/>" +
            "<a href='{0}'>{0}</a>",
            url,
            cadastro.EMAIL
            );

            /*
            * MailMessage -> Construir a mensagem
            */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(cadastro.EMAIL);
            mensagem.Subject = "Sesc Amapá - Recuperação de Senha ";
            mensagem.Body = msgBody;
            mensagem.IsBodyHtml = true;

            /*
            * SendMessage
            */
            _smtp.Send(mensagem);
        }

        public void EnviaComprovante(CadastroLoginSescAP cadastro)
        {
            string msgBody = string.Format(
            "<h2>Comprovante de recarga - Sesc Amapá</h2>" +
            "<h3>Prezado Cliente, {0}</h3> <br/>" +
            "<h4>Segue em anexo o comprovante da recarga.</h4> <br/>",
            cadastro.EMAIL
            );

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(cadastro.EMAIL);
            mensagem.Subject = "Sesc Amapá - Comprovante de Recarga ";
            mensagem.Body = msgBody;
            mensagem.IsBodyHtml = true;

            string wwwRootPath = _hostingEnv.WebRootPath;
            string filePath = Path.Combine(wwwRootPath, "comprovante", "comprovante_de_recarga.pdf");

            Attachment anexo = new(filePath);
            mensagem.Attachments.Add(anexo);

            _smtp.Send(mensagem);
        }
    }
}
