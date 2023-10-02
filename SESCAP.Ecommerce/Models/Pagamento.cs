using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cielo;
using SESCAP.Ecommerce.Libraries.Lang;

namespace SESCAP.Ecommerce.Models
{
    
    public class Pagamento
    {

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        [Display(Name = "Forma de pagamento")]
        [EnumDataType(typeof(PaymentType))]
        public PaymentType TipoPagamento { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        [Range(1, double.PositiveInfinity, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E06")]
        public decimal Valor { get; set; }

    }
}
