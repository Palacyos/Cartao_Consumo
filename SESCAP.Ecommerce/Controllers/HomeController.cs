using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Libraries.Email;
using SESCAP.Ecommerce.Libraries.Filtros;
using SESCAP.Ecommerce.Libraries.Login;
using SESCAP.Ecommerce.Libraries.Seguranca;
using SESCAP.Ecommerce.Models;
using SESCAP.Ecommerce.Repositorios;

namespace SESCAP.Ecommerce.Controllers
{
    public class HomeController : Controller
    {

        private ICadastroLoginRepositorio CadastroLoginRepositorio { get; }
        private IClientelaRepositorio ClientelaRepositorio { get; }
        private ICartaoRepositorio CartaoRepositorio { get; }
        private GerenciarEmail GerenciarEmail { get; }
        private LoginClientela LoginClientela { get; }
        private ISaldoCartaoRepositorio SaldoCartaoRepositorio { get; }
        private IProdutoPdvRepositorio ProdutoPdvRepositorio { get; }
        private IConfiguration Configuration { get; }
       
        public HomeController(ICadastroLoginRepositorio cadastroLoginRepositorio, LoginClientela loginClientela, IClientelaRepositorio clientelaRepositorio, ICartaoRepositorio cartaoRepositorio, GerenciarEmail gerenciarEmail, ISaldoCartaoRepositorio saldoCartaoRepositorio,
            IProdutoPdvRepositorio produtoPdvRepositorio, IConfiguration configuration)
        {
            ClientelaRepositorio = clientelaRepositorio;
            CadastroLoginRepositorio = cadastroLoginRepositorio;
            LoginClientela = loginClientela;
            CartaoRepositorio = cartaoRepositorio;
            GerenciarEmail = gerenciarEmail;
            SaldoCartaoRepositorio = saldoCartaoRepositorio;
            ProdutoPdvRepositorio = produtoPdvRepositorio;
            Configuration = configuration;

           
        }


        [HttpGet]
        public IActionResult CadastroLoginClientela()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CadastroLoginClientela([FromForm] CadastroLoginSescAP cadastro)
        {

            if (ModelState.IsValid)
            {
                var matriculaCadastro = CadastroLoginRepositorio.VerificarCadastro(cadastro.MATRICULA);

                if (matriculaCadastro == null)
                {
                    var clientela = ClientelaRepositorio.ValidaMatricula(cadastro.MATRICULA, cadastro.CPF);

                    if (clientela != null)
                    {
                        if (clientela.DTVENCTO < DateTime.Now)
                        {
                            TempData["MSG_E_CredencialVencida"] = "Sua Credencial Sesc está vencida, procure uma de nossas unidades e renove sua carteirinha.";

                            return RedirectToAction(nameof(CadastroLoginClientela));
                        }

                        CadastroLoginRepositorio.CadastroLogin(cadastro);

                        TempData["MSG_CadastroSucesso"] = "Cadastro realizado com sucesso!";

                        return RedirectToAction(nameof(Login));
                    }
                    TempData["MSG_UsuarioSemCredencial"] = "Usuário não possui a credencial Sesc, Dirija-se a uma de nossas unidades e faça já a sua.";

                    return RedirectToAction(nameof(CadastroLoginClientela));
                }
                TempData["MSG_UsuarioCadastrado"] = $"A matrícula {cadastro.MATRICULA} já possui cadastro. Clique em ENTRAR e faça o login, se necessário altere sua senha caso tenha perdido.";
            }
            return RedirectToAction(nameof(CadastroLoginClientela));
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidaCPF(CadastroLoginSescAP cadastroCpf)
        {
            CPFCNPJ.IMain checkCPF = new CPFCNPJ.Main();
            var resultCpf = checkCPF.IsValidCPFCNPJ(cadastroCpf.CPF);

            if(resultCpf == false)
            {
                return Json($"O CPF {cadastroCpf.CPF} é Inválido");
            }
            return Json(true);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] CadastroLoginSescAP login)
        {

            var cadastroLogin = CadastroLoginRepositorio.Login(login.MATRICULA, login.SENHA);

            if (cadastroLogin != null)
            {
                var obterCadastro = CadastroLoginRepositorio.ObterCadastro(cadastroLogin.ID);

                var clientela = ClientelaRepositorio.ValidaMatricula(obterCadastro.MATRICULA, obterCadastro.CPF);

                if (clientela.DTVENCTO < DateTime.Now)
                {
                    TempData["MSG_E_CredencialVencida"] = "Sua Credencial Sesc venceu, procure uma de nossas unidades e renove sua carteirinha.";

                    return RedirectToAction(nameof(Login));
                }


                LoginClientela.Entrar(clientela);

                return new RedirectResult(Url.Action(nameof(CredencialClientela)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique a MATRÍCULA e a SENHA digitado.";

                return View();

            }

        }

        [ClientelaAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            LoginClientela.Sair();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarSenha([FromForm] CadastroLoginSescAP login)
        {
          
            var cadastro = CadastroLoginRepositorio.ObterCadastroPorEmail(login.EMAIL);

            if (cadastro != null)
            {
               
                DateTime expiracao = DateTime.Now.AddMinutes(20);
                var stringExpiracao = expiracao.ToString("yyyy-MM-ddTHH:mm:ss");

                var idCrip =  StringCipher.Encrypt($"{cadastro.ID}|{stringExpiracao}");
                
                var textoSeguroParaUrl = idCrip.Replace("/","-").Replace("+","_").Replace("%","~");

                GerenciarEmail.LinkResetarSenha(cadastro, textoSeguroParaUrl);
                TempData["MSG_S_RecuperaSenha"] = "Link enviado com sucesso.";

                ModelState.Clear();
            }
            else
            {
                TempData["MSG_E_RecuperaSenha"] = "E-mail não cadastrado."; 
            }
            return View();
        }


        [HttpGet]
        public IActionResult CriarSenha(string id)
        {
            try
            {
                var textoSeguroParaUrl = id.Replace("-","/").Replace("_","+").Replace("~","%");
                var idCadastroDecrip = StringCipher.Decrypt(textoSeguroParaUrl);
                string[] dados = idCadastroDecrip.Split('|');
                int idCadastro;

                if (int.TryParse(dados[0], out idCadastro))
                {
                    DateTime expiracao = DateTime.Parse(dados[1]);

                    if( DateTime.Now > expiracao)
                    {
                        TempData["MSG_E_RecuperaSenha"] = "Link de alteração de senha expirado.";
                        return RedirectToAction(nameof(RecuperarSenha));
                    }
                    
                }

                if(!int.TryParse(dados[0], out idCadastro))
                {
                    TempData["MSG_E_RecuperaSenha"] = "Link de alteração de senha inválido.";
                    return RedirectToAction(nameof(RecuperarSenha));
                }

                return View();
            }
            catch (FormatException)
            {
                TempData["MSG_E_RecuperaSenha"] = "Link de alteração de senha inválido.";
                return RedirectToAction(nameof(RecuperarSenha));
            }

            
        }

        [HttpPost]
        public IActionResult CriarSenha([FromForm] CadastroLoginSescAP cadastroLogin , string id)
        {

            ModelState.Remove("MATRICULA");
            ModelState.Remove("CPF");
            ModelState.Remove("EMAIL");

            if (ModelState.IsValid)
            {
                int idCadastro;
                try
                {
                    var textoSeguroParaUrl = id.Replace("-","/").Replace("_","+").Replace("~","%");
                    var idCadastroDecrip = StringCipher.Decrypt(textoSeguroParaUrl);
                    string[] dados = idCadastroDecrip.Split('|');

                    if (!int.TryParse(dados[0], out idCadastro))
                    {

                        TempData["MSG_E_CriarNovaSenha"] = "Link de alteração de senha inválido.";
                        return View();

                    }
                  
                }
                catch (FormatException)
                {
                    TempData["MSG_E_CriarNovaSenha"] = "Link de alteração de senha inválido.";
                    return View();
                }

                var cadastroDb = CadastroLoginRepositorio.ObterCadastro(idCadastro);

                if (cadastroDb != null)
                {
                    cadastroDb.SENHA = cadastroLogin.SENHA;

                    CadastroLoginRepositorio.Atualizar(cadastroDb);

                    TempData["MSG_CadastroSucesso"] = "Senha alterada com sucesso.";

                }

            }
            return RedirectToAction(nameof(Login));

        }

        [HttpGet]
        [ClientelaAutorizacao]
        public IActionResult CredencialClientela()
        {
            var clientelaLogin = LoginClientela.Obter();

            var clientela = ClientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);

            var cartao = CartaoRepositorio.Cartao(clientela.CARTAO.NUMCARTAO);

            var produtoPdv = ProdutoPdvRepositorio.ObterProdutoPdv(Configuration.GetValue<int>("ProdutoPdvRecargaCartao"));

            var saldo = SaldoCartaoRepositorio.ObterSaldoCartao(cartao.NUMCARTAO, produtoPdv.CDPRODUTO);

            ViewBag.ObterSaldo = saldo;
         
            return View(cartao);


        }

        [HttpGet]
        [ClientelaAutorizacao]
        public IActionResult MovimentacaoCredencialClientela()
        {

            var clientelaLogin = LoginClientela.Obter();

            var clientela = ClientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);

            var cartao = CartaoRepositorio.Cartao(clientela.CARTAO.NUMCARTAO);

            return View(cartao);
        }

        [HttpGet]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorView();

            if(id == 500)
            {
                modelErro.Mensagem = "Tente novamente mais tarde ou entre contato com uma de nossas unidades.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;

            }
            else if(id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe!";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if(id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado.";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);

        }

    }
}