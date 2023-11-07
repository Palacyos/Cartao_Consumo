using System.ComponentModel.DataAnnotations;
using SESCAP.Ecommerce.Libraries.Lang;

namespace SESCAP.Ecommerce.Models
{

    public class CartaoPagamento
    {
        [Display(Name = "Número do Cartão")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        [CreditCard(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E03")]
        [MinLength(18, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E02")]
        [MaxLength(19, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E05")]
        public string NumeroCartao { get; set; }

        [Display(Name = "Nome no Cartão")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        public string NomeNoCartao { get; set; }

        [Display(Name = "Mês do Vencimento")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        public string VencimentoMM { get; set; }

        [Display(Name = "Ano do Vencimento")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        public string VencimentoYY { get; set; }

        [Display(Name = "Código de Segurança")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        public string CodigoSeguranca { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        [Display(Name = "Bandeira do cartão")]
        public string Bandeira { get; set; }

    }
}
