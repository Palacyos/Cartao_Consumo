using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using Cielo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyApp.Namespace;
using Newtonsoft.Json;
using SESCAP.Ecommerce.Libraries.Filtros;
using SESCAP.Ecommerce.Libraries.GeradorQRCode;
using SESCAP.Ecommerce.Libraries.Login;
using SESCAP.Ecommerce.Libraries.Pagamento.Cielo;
using SESCAP.Ecommerce.Models;
using SESCAP.Ecommerce.Models.Constantes;
using SESCAP.Ecommerce.Repositorios;

namespace SESCAP.Ecommerce;

[ClientelaAutorizacao]
public class CobrancaController: Controller
{
    private readonly LoginClientela _loginClientela;
    private readonly IClientelaRepositorio _clientelaRepositorio;
    private readonly IPagamentoOnlineRepositorio _pagamentoOnlineRepositorio;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _hostingEnv;
    private readonly GerenciarCielo _gerenciarCielo;
    private readonly IPagamentoOnlineRepositorio _pgonline;
    private readonly ICapdvRepositorio _capdvRepositorio;
    private readonly ICacaixaRepositorio _cacaixaRepositorio;
    private readonly IPagamentosRepositorio _pagamentosRepositorio;
    private readonly ICobrancaRepositorio _cobrancaRepositorio;


    public CobrancaController(LoginClientela loginClientela, IClientelaRepositorio clientelaRepositorio, IConfiguration configuration, IMapper mapper,
    IPagamentoOnlineRepositorio pagamentoOnlineRepositorio, IWebHostEnvironment hostingEnv, GerenciarCielo gerenciarCielo, IPagamentoOnlineRepositorio pgonline, ICapdvRepositorio capdvRepositorio,
    ICacaixaRepositorio cacaixaRepositorio, IPagamentosRepositorio pagamentosRepositorio, ICobrancaRepositorio cobrancaRepositorio)
    {
        _loginClientela = loginClientela;
        _clientelaRepositorio = clientelaRepositorio;
        _configuration = configuration;
        _mapper = mapper;
        _pagamentoOnlineRepositorio = pagamentoOnlineRepositorio;
        _hostingEnv = hostingEnv;
        _gerenciarCielo = gerenciarCielo;
        _pgonline = pgonline;
        _capdvRepositorio = capdvRepositorio;
        _cacaixaRepositorio = cacaixaRepositorio;
        _pagamentosRepositorio = pagamentosRepositorio;
        _cobrancaRepositorio = cobrancaRepositorio;
    } 



    [HttpGet]
    public IActionResult CobrancaClientela()
    {
        var clientelaLogin = _loginClientela.Obter();
        var clientela = _clientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);

        ViewBag.ClientelaImg = clientela.CarregaFoto;

        var pcDesconto = _configuration.GetValue<decimal>("DescontoGeralEdu");
        var cdEducacaoInfantil = _configuration.GetValue<int>("EINFANTIL");
        var cdEducacaoFundamental = _configuration.GetValue<int>("EFUNDAMENTAL");
        var descontoFuncNovo = _configuration.GetValue<int>("CDFUNCNOVO");
        var descontoFuncVet = _configuration.GetValue<int>("CDFUNCVET");
        

        TempData["Educacao_Infantil"] = cdEducacaoInfantil.ToString();
        ViewBag.EducacaoInfantil = TempData["Educacao_Infantil"] as string;

        TempData["Educacao_Fundamental"] = cdEducacaoFundamental.ToString();
        ViewBag.EducacaoFundamental = TempData["Educacao_Fundamental"] as string;

        var clientelaCobrancasViewModel = new List<ClientelaCobrancaViewModel>();

        foreach(var cobranca in clientela.COBRANCAS)
        {
            var clientelaCobrancaViewModel = new ClientelaCobrancaViewModel
            {
                DsCobranca = cobranca.DSCOBRANCA,
                DtVencto = cobranca.DTVENCTO,
                Valor = cobranca.VLCOBRADO,
                StRecebido = cobranca.STRECEBIDO,
                IdClasse = cobranca.IDCLASSE,
                CdElement = cobranca.CDELEMENT,
                Sqcobranca = cobranca.SQCOBRANCA
            };
            
            if(ElementoEducacional(cobranca.CDELEMENT, cdEducacaoFundamental, cdEducacaoInfantil))
            {
                var juros = CalcularJuros(cobranca.Juros, cobranca.VLCOBRADO, cobranca.DTVENCTO);
                var multa = CalcularMulta(cobranca.Multa, cobranca.VLCOBRADO, cobranca.DTVENCTO);
                var valorDesconto = CalcularDesconto(pcDesconto, cobranca.VLCOBRADO);
                var valorJuros = juros + multa;
                var vlDesc = Math.Round(valorDesconto, 2);
                var vlJuros = Math.Round(valorJuros, 2);

                if(DateTime.Today <= cobranca.DTVENCTO && !ColaboradorComDesconto(clientela.INSCRICOES, descontoFuncNovo, descontoFuncVet))
                {
                    clientelaCobrancaViewModel.Valor -= vlDesc;
                    clientelaCobrancaViewModel.ValorDesconto = vlDesc;
                }
                else if(DateTime.Today > cobranca.DTVENCTO && ColaboradorComDesconto(clientela.INSCRICOES, descontoFuncNovo, descontoFuncVet))
                {
                    var valorDobrado = clientelaCobrancaViewModel.Valor + clientelaCobrancaViewModel.Valor;
                    var jurosDobrado = CalcularJuros(cobranca.Juros, valorDobrado, cobranca.DTVENCTO);
                    var multaValorDobrado = CalcularMulta(cobranca.Multa,valorDobrado, cobranca.DTVENCTO);
                    var valorJurosDobrado = jurosDobrado + multaValorDobrado;
                    var vlJurosDob = Math.Round(valorJurosDobrado,2);

                    clientelaCobrancaViewModel.Valor = valorDobrado + vlJurosDob;
                    clientelaCobrancaViewModel.ValorJuros = vlJurosDob;
                }
                else
                {
                    clientelaCobrancaViewModel.Valor += vlJuros;
                    clientelaCobrancaViewModel.ValorJuros = vlJuros;
                }
            
            }
            clientelaCobrancasViewModel.Add(clientelaCobrancaViewModel);
        }

        return View(clientelaCobrancasViewModel);
    }

    [HttpGet]
    public IActionResult ObterCobrancasPorStatus(short status)
    {
        var clientelaLogin = _loginClientela.Obter();
        var clientela = _clientelaRepositorio.ObterCobrancasPorStatus(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP, status);

        var cobrancas = clientela.COBRANCAS
        .Select(cob => new ClientelaCobrancaViewModel
        {
            DsCobranca = cob.DSCOBRANCA,
            DtVencto = cob.STRECEBIDO == 1 ? cob.PAGAMENTOS.Where(p => p.CDELEMENT == cob.CDELEMENT && p.SQCOBRANCA == cob.SQCOBRANCA).OrderByDescending(p => p.DTRECEBIDO).Select(p => p.DTRECEBIDO).FirstOrDefault() : cob.DTVENCTO,
            Valor = cob.STRECEBIDO == 1 ? cob.PAGAMENTOS.Where(p => p.CDELEMENT == cob.CDELEMENT && p.SQCOBRANCA == cob.SQCOBRANCA ).Sum(p => p.VLRECEBIDO) : cob.VLCOBRADO,
            CdElement = cob.CDELEMENT,
            StRecebido = cob.STRECEBIDO

        });

        return PartialView("_CobrancaTablePartial", cobrancas); 
    }

    [HttpPost]
    public IActionResult ProcessarPagamento(List<string> selectedCobrancas)
    {
        var cobrancas = new List<ClientelaCobrancaViewModel>();
        var clientelalogin = _loginClientela.Obter();
        var clientela = _clientelaRepositorio.ObterClientela(clientelalogin.SQMATRIC, clientelalogin.CDUOP);
        
        ViewBag.clientelaImg = clientela.CarregaFoto;

        decimal total = 0;        

        foreach(var selected in selectedCobrancas)
        {
            var parts = selected.Split('|');
            if(parts.Length == 7)
            {
                var cobranca = new ClientelaCobrancaViewModel
                {
                    IdClasse = parts[0],
                    CdElement = parts[1],
                    Sqcobranca = int.Parse(parts[2]),
                    Valor = decimal.Parse(parts[3]),
                    ValorDesconto = decimal.Parse(parts[4]),
                    ValorJuros = decimal.Parse(parts[5]),
                    DsCobranca = parts[6]
                    
                };

                cobrancas.Add(cobranca);
                total += cobranca.Valor;
            }
        }
        
        var viewModel = new RecargaViewModel
        {
            Total = total,
            Cobrancas = cobrancas
        };
        
        return View(viewModel);
    }

    
    [HttpGet]
    public IActionResult ConfirmarPagamento(string total, List<string> cobrancasData)
    {

        var clientelaLogin = _loginClientela.Obter();
        var clientela = _clientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);

        ViewBag.ClientelaImg = clientela.CarregaFoto;

        var decryptedTotal = decimal.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(total)));
        var cobrancas = new List<ClientelaCobrancaViewModel>();

        foreach (var encryptedCobranca in cobrancasData)
        {
            var decryptedString = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedCobranca));
            var parts = decryptedString.Split('|');
            if (parts.Length == 7)
            {
                var cobranca = new ClientelaCobrancaViewModel
                {
                    IdClasse = parts[0],
                    CdElement = parts[1],
                    Sqcobranca = int.Parse(parts[2]),
                    Valor = decimal.Parse(parts[3]),
                    ValorDesconto = decimal.Parse(parts[4]),
                    ValorJuros = decimal.Parse(parts[5]),
                    DsCobranca = parts[6]
                };

                cobrancas.Add(cobranca);
            }
        }

        var viewModel = new RecargaViewModel
        {
            Total = decryptedTotal,
            Cobrancas = cobrancas
        };

        ViewBag.TipoPagamento = new[]
        {
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

        return View(viewModel);
       
    }

    [HttpPost]
    public IActionResult ConfirmarPagamento([FromForm] RecargaViewModel model)
    {
        var total = Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Total.ToString()));
        var cobrancasData = model.Cobrancas.Select(
        c => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{c.IdClasse}|{c.CdElement}|{c.Sqcobranca}|{c.Valor}|{c.ValorDesconto}|{c.ValorJuros}|{c.DsCobranca}"))).ToList();

        if(ModelState.IsValid)
        {
            try
            {
                Transaction transacaoPagamento =  _gerenciarCielo.GerarPagamentoRecargaCartaoDeCredito(model);

                if (transacaoPagamento.Payment.GetStatus() == Status.PaymentConfirmed)
                {
                    PagamentoOnline pgOnline = SalvarPagamento(transacaoPagamento);

                    TempData["Pagamento_MSG"] = "Pagamento Realizado Com Sucesso";

                    var capdv = _capdvRepositorio.ObterCaixaPdv(_configuration.GetValue<int>("CAPDV"));

                    var caixa = _cacaixaRepositorio.ObterCaixaAberto(capdv.CDPDV);

                    if(caixa == null)
                    {
                        caixa = _cacaixaRepositorio.AbreCaixa(capdv.CDPDV, _configuration.GetValue<int>("CdPessoa"), capdv.LOCALVENDA.UOP.CDUOP, capdv.NMESTACAO, capdv.LOCALVENDA.CDLOCVENDA);
                    }

                    var cxPagamento = _cacaixaRepositorio.CaixaDeposito(caixa.SQCAIXA, caixa.CDPESSOA);

                    foreach (var cobranca in model.Cobrancas)
                    {
                        _pagamentosRepositorio.CadastraPagamento(cxPagamento.SQCAIXA, _configuration.GetValue<int>("CIELOCREDITO"), cobranca.IdClasse, cobranca.CdElement,
                        cobranca.Sqcobranca, cobranca.Valor, cobranca.ValorJuros, _configuration.GetValue<string>("DsPgamento"),
                        cobranca.ValorDesconto, cxPagamento.CDUOP, cxPagamento.CDPESSOA);

                        var cob = _cobrancaRepositorio.ObterCobranca(cobranca.IdClasse, cobranca.CdElement, cobranca.Sqcobranca);
                        _cobrancaRepositorio.AtualizaSituacaoRecebidoCobranca(cob);                       
                    }

                    _cacaixaRepositorio.AtualizaSaldoCaixa(cxPagamento, pgOnline.Total);

                    return new RedirectToActionResult("ConfirmacaoPagamentoCobranca", "Cobranca", new { id = pgOnline.Id });
                }

                TempData["Pagamento_MSG_ERRO"] = "Erro no pagamento";

                ViewBag.Pagamento_MSG_ERRO = TempData["Pagamento_MSG_ERRO"] as string;

                var msgPagemnto = _hostingEnv.IsDevelopment() ? transacaoPagamento.Payment.ReturnMessage : transacaoPagamento.Payment.ReasonMessage;

                TempData["TIPO_ERRO_MSG_PAGAMENTO"] = $"Erro no pagamento, verifique os dados do cartão. ({msgPagemnto})";

                return ConfirmarPagamento(total , cobrancasData);
            }
            catch (CieloException e )
            {
                TempData["MSG_E_Pagamento"] = e.GetCieloErrorsString();

                return ConfirmarPagamento(total , cobrancasData);
            }

        }
        else
        {
            return ConfirmarPagamento(total , cobrancasData);
        }
    }

    [HttpPost]
    public IActionResult ConfirmarPagamentoPix(string total, List<string> cobrancasData)
    {
        var clientelaLogin = _loginClientela.Obter();
        var clientela = _clientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);

        ViewBag.ClientelaImg = clientela.CarregaFoto;

        var decryptedTotal = decimal.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(total)));
        var cobrancas = new List<ClientelaCobrancaViewModel>();
        List<string> decryptCobrancasData = new List<string>();

        foreach (var criptoCobrancas in cobrancasData)
        {
            decryptCobrancasData.Add(Encoding.UTF8.GetString(Convert.FromBase64String(criptoCobrancas)));
        }

        foreach (var encryptedCobrancas in cobrancasData)
        {
            var decryptedCobrancas = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedCobrancas));
            var parts = decryptedCobrancas.Split('|');
            if (parts.Length == 7)
            {
                var cobranca = new ClientelaCobrancaViewModel
                {
                    IdClasse = parts[0],
                    CdElement = parts[1],
                    Sqcobranca = int.Parse(parts[2]),
                    Valor = decimal.Parse(parts[3]),
                    ValorDesconto = decimal.Parse(parts[4]),
                    ValorJuros = decimal.Parse(parts[5]),
                    DsCobranca = parts[6]
                };

                cobrancas.Add(cobranca);
            }

           
        }

        var model = new RecargaViewModel
        {
            Total = decryptedTotal,
            Cobrancas = cobrancas
        };

        try
        {
            Transaction transacaoPagamento = _gerenciarCielo.GerarPagamentoCobrancaPix(model);

            if (transacaoPagamento.Payment.GetStatus() == Status.Pending)
            {
                var json = JsonConvert.SerializeObject(model);
                var modelCripto = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

                return new RedirectToActionResult("QrCodeCobrancaPix", "Cobranca", new {paymentId = transacaoPagamento.Payment.PaymentId.Value, qrCodeString = transacaoPagamento.Payment.QrCodeString, data = modelCripto });   
            }

            return View();
        }
        catch (CieloException e)
        {
            TempData["MSG_E_Pagamento"] = e.GetCieloErrorsString();

            return ProcessarPagamento(decryptCobrancasData);
        }
       
    }

    [HttpGet]
    public IActionResult QrCodeCobrancaPix(Guid paymentId, string qrCodeString, string data)
    {
        var clientelaLogin = _loginClientela.Obter();
        var clientela = _clientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);

        ViewBag.ClientelaImg = clientela.CarregaFoto;

        try
        {
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(data));
            var model = JsonConvert.DeserializeObject<RecargaViewModel>(json);

            var result = _gerenciarCielo.VerificarStatusPagamentoPix(paymentId);

            if(result.Payment.GetStatus() == Status.Pending)
            {
                var qrCodeImagem = GeradorQrCode.GerarImagem(qrCodeString);

                ViewBag.QrCodeImg = qrCodeImagem;
                ViewBag.ChavePix = qrCodeString;
                ViewBag.StatusPagamento = result.Payment.Status;

                return View();
            }

            PagamentoOnline pgOnline = SalvarPagamento(result);

            TempData["Pagamento_MSG"] = "Pagamento Realizado Com Sucesso";

            var capdv = _capdvRepositorio.ObterCaixaPdv(_configuration.GetValue<int>("CAPDV"));

            var caixa = _cacaixaRepositorio.ObterCaixaAberto(capdv.CDPDV);

            if(caixa == null)
            {
                caixa = _cacaixaRepositorio.AbreCaixa(capdv.CDPDV, _configuration.GetValue<int>("CdPessoa"), capdv.LOCALVENDA.UOP.CDUOP, capdv.NMESTACAO, capdv.LOCALVENDA.CDLOCVENDA);
            }

            var cxPagamento = _cacaixaRepositorio.CaixaDeposito(caixa.SQCAIXA, caixa.CDPESSOA);

            foreach (var cobranca in model.Cobrancas)
            {
                _pagamentosRepositorio.CadastraPagamento(cxPagamento.SQCAIXA, _configuration.GetValue<int>("PixCielo"), cobranca.IdClasse, cobranca.CdElement,
                cobranca.Sqcobranca, cobranca.Valor, cobranca.ValorJuros, _configuration.GetValue<string>("DsPgamento"),
                cobranca.ValorDesconto, cxPagamento.CDUOP, cxPagamento.CDPESSOA);

                var cob = _cobrancaRepositorio.ObterCobranca(cobranca.IdClasse, cobranca.CdElement, cobranca.Sqcobranca);
                _cobrancaRepositorio.AtualizaSituacaoRecebidoCobranca(cob);                       
            }

            _cacaixaRepositorio.AtualizaSaldoCaixa(cxPagamento, pgOnline.Total);

            return new RedirectToActionResult("ConfirmacaoPagamentoCobranca", "Cobranca", new { id = pgOnline.Id });
        }
        catch (CieloException e)
        {
            TempData["MSG_E_Pagamento"] = e.GetCieloErrorsString();

            return View();
        }
    }

    [HttpGet]
    public IActionResult ConfirmacaoPagamentoCobranca(int id) 
    {

        var clientelaLogin = _loginClientela.Obter();
        var clientela = _clientelaRepositorio.ObterClientela(clientelaLogin.SQMATRIC, clientelaLogin.CDUOP);
        
        ViewBag.ClientelaImg = clientela.CarregaFoto;

        PagamentoOnline pgOnline = _pgonline.ObterPagamento(id);

        ViewBag.Pagamento_MSG = TempData["Pagamento_MSG"] as string;

        return View(pgOnline);
    }

    private decimal CalcularJuros(decimal juros, decimal valor, DateTime dtVencto)
    {
        decimal jurosDiario = juros;
        DateTime hoje = DateTime.Today;
        int diasEmAtraso = (hoje - dtVencto).Days; 
        diasEmAtraso = diasEmAtraso < 0 ? 0 : diasEmAtraso;

        if(diasEmAtraso > 0)
        {
            return valor * jurosDiario * diasEmAtraso;
        }

        return 0;
    }

    private decimal CalcularMulta(decimal multa, decimal valor, DateTime dtVencto)
    {
        DateTime hoje = DateTime.Today;
        int diasEmAtraso = (hoje - dtVencto).Days; 
        diasEmAtraso = diasEmAtraso < 0 ? 0 : diasEmAtraso;

        if(diasEmAtraso > 0)
        {
            return valor * multa;
        }

        return 0;

    }

    private decimal CalcularDesconto(decimal desconto, decimal valor)
    {
        return valor * desconto;
    }

    private bool ElementoEducacional(string cdElement, int cdEducacaoFundamental, int cdEducacaoInfantil)
    {
        var elementSubstring = cdElement.Substring(1,7);
        return elementSubstring == cdEducacaoFundamental.ToString() || elementSubstring == cdEducacaoInfantil.ToString();

    }

    private bool ColaboradorComDesconto(IEnumerable<INSCRICAO> inscricoes, int descontoFuncNovo, int descontoFunVet)
    {
        return inscricoes.Any( i => i.CDFORMATO == descontoFuncNovo || i.CDFORMATO == descontoFunVet);
    }
    
    private PagamentoOnline SalvarPagamento(Transaction transacao)
    {
        var clientelaLogin = _loginClientela.Obter();

        PagamentoOnline pgOnline = _mapper.Map<PagamentoOnline>(transacao);
        pgOnline.SQMATRIC = clientelaLogin.SQMATRIC;
        pgOnline.CDUOP = clientelaLogin.CDUOP;
        pgOnline.Status = StatusConstante.Sucesso;

        _pagamentoOnlineRepositorio.Cadastrar(pgOnline);

        return pgOnline;
    }

}
