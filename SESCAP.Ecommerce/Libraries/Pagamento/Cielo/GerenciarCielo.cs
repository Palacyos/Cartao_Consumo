using System;
using Cielo;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SESCAP.Ecommerce.Libraries.Login;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Libraries.Pagamento.Cielo
{
    public class GerenciarCielo
    {
        private IConfiguration Configuration { get; }
        private LoginClientela LoginClientela { get; }
        private readonly IWebHostEnvironment _env;

        public GerenciarCielo(IConfiguration configuration, LoginClientela loginClientela, IWebHostEnvironment env)
        {
            Configuration = configuration;
            LoginClientela = loginClientela;
            _env = env;
        }

       
        public Transaction GerarPagamentoRecargaCartaoDeCredito(RecargaViewModel recargaViewModel)
        {

            CLIENTELA clientela = LoginClientela.Obter();

            Merchant merchant = new Merchant(Configuration.GetValue<Guid>("Cielo:MerchantId"), Configuration.GetValue<string>("Cielo:MerchantKey"));

            ISerializerJSON json = new SerializerJSON();

            CieloApi apiCielo;

            if(_env.IsDevelopment())
            {
                apiCielo = new CieloApi(CieloEnvironment.SANDBOX, merchant, json);
            }
            else
            {
                apiCielo = new CieloApi(CieloEnvironment.PRODUCTION, merchant, json);
            }
          
            var descricaoFatura = Configuration.GetValue<string>("DescricaoFaturaRecarga");


            var customer = new Customer(clientela.NMCLIENTE);
            customer.SetIdentityType(IdentityType.CPF);
            customer.Identity = clientela.NUCPF;

            var creditCard = new Card();
            creditCard.SecurityCode = recargaViewModel.CartaoPagamento.CodigoSeguranca;
            creditCard.ExpirationDate = recargaViewModel.CartaoPagamento.VencimentoMM + "/" + recargaViewModel.CartaoPagamento.VencimentoYY;
            creditCard.Holder = recargaViewModel.CartaoPagamento.NomeNoCartao;
            creditCard.CardNumber = recargaViewModel.CartaoPagamento.NumeroCartao.Replace(" ", "");
            creditCard.Brand = recargaViewModel.CartaoPagamento.Bandeira;
            

            var payment = new Payment(
            amount: recargaViewModel.Pagamento.Valor,
            currency: Currency.BRL,
            paymentType: recargaViewModel.Pagamento.TipoPagamento,
            installments: 1,
            capture: true,
            softDescriptor: descricaoFatura,
            card: creditCard);
           

            var merchantOrderId = new Random().Next();


            var transaction = new Transaction(
            merchantOrderId: merchantOrderId.ToString(),
            customer: customer,
            payment: payment

            );


            return apiCielo.CreateTransaction(Guid.NewGuid(), transaction);

        }

        public Transaction GerarPagamentoRecargaPix(RecargaViewModel recargaViewModel)
        {
            CLIENTELA clientela = LoginClientela.Obter();

            Merchant merchant = new Merchant(Configuration.GetValue<Guid>("Cielo:MerchantId"), Configuration.GetValue<string>("Cielo:MerchantKey"));

            ISerializerJSON json = new SerializerJSON();

            CieloApi apiCielo;

            if(_env.IsDevelopment())
            {
                apiCielo = new CieloApi(CieloEnvironment.SANDBOX, merchant, json);
            }
            else
            {
                apiCielo = new CieloApi(CieloEnvironment.PRODUCTION, merchant, json);
            }


            var customer = new Customer(clientela.NMCLIENTE);
            customer.SetIdentityType(IdentityType.CPF);
            customer.Identity = clientela.NUCPF;


            var payment = new Payment();
            payment.SetAmount(recargaViewModel.Pagamento.Valor);
            payment.Type = "Pix";

            var merchantOrderId = new Random().Next();

            var transaction = new Transaction(
            merchantOrderId: merchantOrderId.ToString(),
            customer: customer,
            payment: payment);

            return apiCielo.CreateTransaction(Guid.NewGuid(), transaction);


        }

        public Transaction GerarPagamentoCobrancaPix(RecargaViewModel recargaViewModel)
        {
            CLIENTELA clientela = LoginClientela.Obter();

            Merchant merchant = new Merchant(Configuration.GetValue<Guid>("Cielo:MerchantId"), Configuration.GetValue<string>("Cielo:MerchantKey"));

            ISerializerJSON json = new SerializerJSON();

            CieloApi apiCielo;

            if(_env.IsDevelopment())
            {
                apiCielo = new CieloApi(CieloEnvironment.SANDBOX, merchant, json);
            }
            else
            {
                apiCielo = new CieloApi(CieloEnvironment.PRODUCTION, merchant, json);
            }


            var customer = new Customer(clientela.NMCLIENTE);
            customer.SetIdentityType(IdentityType.CPF);
            customer.Identity = clientela.NUCPF;


            var payment = new Payment();
            payment.SetAmount(recargaViewModel.Total);
            payment.Type = "Pix";

            var merchantOrderId = new Random().Next();

            var transaction = new Transaction(
            merchantOrderId: merchantOrderId.ToString(),
            customer: customer,
            payment: payment);

            return apiCielo.CreateTransaction(Guid.NewGuid(), transaction);


        }

        public Transaction VerificarStatusPagamentoPix(Guid paymentId)
        {
            Merchant merchant = new Merchant(Configuration.GetValue<Guid>("Cielo:MerchantId"), Configuration.GetValue<string>("Cielo:MerchantKey"));

            ISerializerJSON json = new SerializerJSON();

            CieloApi apiCielo;

            if(_env.IsDevelopment())
            {
                apiCielo = new CieloApi(CieloEnvironment.SANDBOX, merchant, json);
            }
            else
            {
                apiCielo = new CieloApi(CieloEnvironment.PRODUCTION, merchant, json);
            }

            return apiCielo.GetTransaction(paymentId);
        }

    }
}
