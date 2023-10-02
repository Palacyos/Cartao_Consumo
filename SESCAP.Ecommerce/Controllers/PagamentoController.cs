using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cielo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SESCAP.Ecommerce.Libraries.Filtros;
using SESCAP.Ecommerce.Libraries.Login;
using SESCAP.Ecommerce.Libraries.Pagamento.Cielo;
using SESCAP.Ecommerce.Models;
using SESCAP.Ecommerce.Models.Constantes;
using SESCAP.Ecommerce.Repositorios;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Hangfire;
using Hangfire.Common;

namespace SESCAP.Ecommerce.Controllers
{
    [ClientelaAutorizacao]
    public class PagamentoController : Controller
    {

        private IClientelaRepositorio ClientelaRepositorio { get; }
        private ICartaoRepositorio CartaoRepositorio { get; }
        private IMoedaPgtoRepositorio MoedaPgtoRepositorio { get; }
        private GerenciarCielo GerenciarCielo { get; }
        private IPagamentoOnlineRepositorio PagamentoOnlineRepositorio { get; }
        private LoginClientela LoginClientela { get; }
        private ICacaixaRepositorio CacaixaRepositorio { get; }
        private ICxDepRetPdvRepositorio CxDepRetPdvRepositorio { get; }
        private ICapdvRepositorio CapdvRepositorio { get; }
        private IProdutoPdvRepositorio ProdutoPdvRepositorio { get; }
        private ISaldoCartaoRepositorio SaldoCartaoRepositorio { get; }
        private IHstMovCartReposiotrio HstMovCartReposiotrio { get; }
        private IConfiguration Configuration { get; }
        private ITarefaRecorrente TarefaRecorrente { get; }
        private readonly IMapper _mapper;


        public PagamentoController(IClientelaRepositorio clientelaRepositorio, ICartaoRepositorio cartaoRepositorio, LoginClientela loginClientela, GerenciarCielo gerenciarCielo,
            IMoedaPgtoRepositorio moedaPgtoRepositorio, IPagamentoOnlineRepositorio pagamentoOnlineRepositorio, IMapper mapper, ICacaixaRepositorio cacaixaRepositorio, ICxDepRetPdvRepositorio cxDepRetPdvRepositorio, ICapdvRepositorio capdvRepositorio,
            IConfiguration configuration, IProdutoPdvRepositorio produtoPdvRepositorio, ISaldoCartaoRepositorio saldoCartaoRepositorio, IHstMovCartReposiotrio hstMovCartReposiotrio, ITarefaRecorrente tarefaRecorrente)
        {
            ClientelaRepositorio = clientelaRepositorio;
            CartaoRepositorio = cartaoRepositorio;
            LoginClientela = loginClientela;
            GerenciarCielo = gerenciarCielo;
            MoedaPgtoRepositorio = moedaPgtoRepositorio;
            PagamentoOnlineRepositorio = pagamentoOnlineRepositorio;
            _mapper = mapper;
            CacaixaRepositorio = cacaixaRepositorio;
            CxDepRetPdvRepositorio = cxDepRetPdvRepositorio;
            CapdvRepositorio = capdvRepositorio;
            Configuration = configuration;
            ProdutoPdvRepositorio = produtoPdvRepositorio;
            SaldoCartaoRepositorio = saldoCartaoRepositorio;
            HstMovCartReposiotrio = hstMovCartReposiotrio;
            TarefaRecorrente = tarefaRecorrente;
           
        }
 


        [HttpGet]
        public IActionResult Recarga()
        {

            var clientelaLogin = LoginClientela.Obter();
            var clientela = ClientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);
            var cartao = CartaoRepositorio.Cartao(clientela.CARTAO.NUMCARTAO);

            ViewBag.CartaoImg = cartao.CLIENTELA.CarregaFoto;

            ViewBag.TipoPagamento = new[]
            {
                new SelectListItem(){Value = "", Text = "Selecione Forma de Pagamento (obrigatório)"},
                new SelectListItem(){Value = "CreditCard", Text = "Crédito"}
            };

            ViewBag.Bandeira = new[]
            {
                new SelectListItem(){Value = "", Text = "Selecione a Bandeira do Cartão (obrigatório)"},
                new SelectListItem(){Value = "Master", Text = "Master"},
                new SelectListItem(){Value = "Visa", Text = "Visa"},
                new SelectListItem(){Value = "Elo", Text = "Elo"},
                new SelectListItem(){Value = "Amex", Text = "American Express"},
                new SelectListItem(){Value = "Diners", Text = "Diners"},
                new SelectListItem(){Value = "JCB", Text = "JCB"},
                new SelectListItem(){Value = "Hipercard", Text = "Hipercard"}
            };

            return View();
        }


        [HttpPost] 
        public IActionResult Recarga([FromForm] RecargaViewModel recargaViewModel)
        {
            


            if (ModelState.IsValid)
            {
                

                try
                {
                    Transaction transacaoPagamento =  GerenciarCielo.GerarPagamentoCartaoDeCreditoRecarga(recargaViewModel);

                    if (transacaoPagamento.Payment.GetStatus() == Status.PaymentConfirmed) {

                       
                        PagamentoOnline pgOnline = SalvarPagamento(transacaoPagamento);

                        TempData["Pagamento_MSG"] = "Pagamento Realizado Com Sucesso";

                        var clientelaLogin = LoginClientela.Obter();
                        var clientela = ClientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);
                        var cartao = CartaoRepositorio.Cartao(clientela.CARTAO.NUMCARTAO);

                        var capdv = CapdvRepositorio.ObterCaixaPdv(Configuration.GetValue<int>("CAPDV"));

                        var produtoPdvRecargaCartao = ProdutoPdvRepositorio.ObterProdutoPdv(Configuration.GetValue<int>("ProdutoPdvRecargaCartao"));

                        var caixa = CacaixaRepositorio.ObterCaixaAberto(capdv.CDPDV);

                        if (caixa == null)
                        {
                            caixa = CacaixaRepositorio.AbreCaixa(capdv.CDPDV,Configuration.GetValue<int>("CdPessoa"), capdv.LOCALVENDA.UOP.CDUOP, capdv.NMESTACAO, capdv.LOCALVENDA.CDLOCVENDA);
                        }
                     
                        var cxDeposito = CacaixaRepositorio.CaixaDeposito(caixa.SQCAIXA, caixa.CDPESSOA);
                      
                        var depRetPdv = CxDepRetPdvRepositorio.CadastraDeposito(cartao.NUMCARTAO, cxDeposito.SQCAIXA, cxDeposito.CDPESSOA, cxDeposito.DTABERTURA, pgOnline.Total, Configuration.GetValue<int>("CIELOCREDITO"));

                        var obterSaldoCartao = SaldoCartaoRepositorio.ObterSaldoCartao(depRetPdv.NUMCARTAO, produtoPdvRecargaCartao.CDPRODUTO);

                        if (obterSaldoCartao != null)
                        {
                            
                            SaldoCartaoRepositorio.AtualizarSaldoCartao(obterSaldoCartao, depRetPdv.VLDEPRET);

                            HstMovCartReposiotrio.InsereMovCartaoConsumo(produtoPdvRecargaCartao.CDPRODUTO, depRetPdv.DTDEPRET, depRetPdv.NUMCARTAO, depRetPdv.VLDEPRET, depRetPdv.SQCAIXA, depRetPdv.CDPESSOA);

                            CacaixaRepositorio.AtualizaSaldoCaixa(cxDeposito, depRetPdv.VLDEPRET);
                                                       
                            return new RedirectToActionResult("ConfirmacaoPagamento", "Pagamento", new { id = pgOnline.Id });
                            
                        }
                        else
                        {
                            SaldoCartaoRepositorio.InsereValorRecarga(cartao.NUMCARTAO, produtoPdvRecargaCartao.CDPRODUTO, depRetPdv.VLDEPRET);

                            HstMovCartReposiotrio.InsereMovCartaoConsumo(produtoPdvRecargaCartao.CDPRODUTO, depRetPdv.DTDEPRET, depRetPdv.NUMCARTAO, depRetPdv.VLDEPRET, depRetPdv.SQCAIXA, depRetPdv.CDPESSOA);

                            CacaixaRepositorio.AtualizaSaldoCaixa(cxDeposito, depRetPdv.VLDEPRET);

                        }

                        return new RedirectToActionResult("ConfirmacaoPagamento", "Pagamento", new { id = pgOnline.Id });

                    }

                    TempData["Pagamento_MSG_ERRO"] = "Erro no pagamento";

                    ViewBag.Pagamento_MSG_ERRO = TempData["Pagamento_MSG_ERRO"] as string;

                    TempData["TIPO_ERRO_MSG_PAGAMENTO"] = "Erro no pagamento, verifique os dados do cartão.";

                    return Recarga();


                }
                catch ( CieloException e )
                {

                    TempData["MSG_E_Pagamento"] = e.GetCieloErrorsString();

                    return Recarga();
                }


            }
            else
            {
                return Recarga();
            }
    
        }


        [HttpGet]
        public IActionResult ConfirmacaoPagamento(int id) 
        {

            var clientelaLogin = LoginClientela.Obter();
            var clientela = ClientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);
            var cartao = CartaoRepositorio.Cartao(clientela.CARTAO.NUMCARTAO);

            ViewBag.CartaoImg = cartao.CLIENTELA.CarregaFoto;

            PagamentoOnline pgOnline = PagamentoOnlineRepositorio.ObterPagamento(id);

            ViewBag.Pagamento_MSG = TempData["Pagamento_MSG"] as string;

            return View(pgOnline);
        }



        private PagamentoOnline SalvarPagamento(Transaction transacao)
        {
            var clientelaLogin = LoginClientela.Obter();

            PagamentoOnline pgOnline = _mapper.Map<PagamentoOnline>(transacao);
            pgOnline.SQMATRIC = clientelaLogin.SQMATRIC;
            pgOnline.CDUOP = clientelaLogin.CDUOP;
            pgOnline.Status = StatusConstante.Sucesso;

            PagamentoOnlineRepositorio.Cadastrar(pgOnline);

            return pgOnline;
        }


    }
}