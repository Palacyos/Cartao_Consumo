using System.Collections.Generic;
using Cielo;

namespace SESCAP.Ecommerce.Models
{
    public class RecargaViewModel
    {
        public CartaoPagamento CartaoPagamento { get; set; }
        public  Pagamento Pagamento { get; set; }
        public Payment Payment { get; set; }
        public decimal Total { get; set; }
        public List<ClientelaCobrancaViewModel> Cobrancas {get; set;}


        public RecargaViewModel()
        {
            Cobrancas = new List<ClientelaCobrancaViewModel>();
        }
    }
}
