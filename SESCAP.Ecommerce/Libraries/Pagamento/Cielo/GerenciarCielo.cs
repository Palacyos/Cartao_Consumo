using System;
using System.Drawing.Text;
using Cielo;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Libraries.Login;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Libraries.Pagamento.Cielo
{
    public class GerenciarCielo
    {
        private IConfiguration Configuration { get; }
        private LoginClientela LoginClientela { get; }

        public GerenciarCielo(IConfiguration configuration, LoginClientela loginClientela)
        {
            Configuration = configuration;
            LoginClientela = loginClientela;
        }

       
        public Transaction GerarPagamentoRecarga(RecargaViewModel recargaViewModel)
        {

            CLIENTELA clientela = LoginClientela.Obter();

            Merchant merchant = new Merchant(Configuration.GetValue<Guid>("PagamentoSandbox:Cielo:MerchantId"), Configuration.GetValue<string>("PagamentoSandbox:Cielo:MerchantKey"));

            ISerializerJSON json = new SerializerJSON();

            var apiCielo = new CieloApi(CieloEnvironment.SANDBOX, merchant, json);

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
           

            var merchanOrderId = new Random().Next();


            var transaction = new Transaction(

                merchantOrderId: merchanOrderId.ToString(),
                customer: customer,
                payment: payment

                );


            return apiCielo.CreateTransaction(Guid.NewGuid(), transaction);

        }
        
        
    }
}
