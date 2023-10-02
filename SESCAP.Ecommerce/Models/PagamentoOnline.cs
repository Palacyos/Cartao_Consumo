using System;
using System.ComponentModel.DataAnnotations.Schema;
using Cielo;

namespace SESCAP.Ecommerce.Models
{
    public class PagamentoOnline
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string FormaPgto { get; set; }
        public decimal Total { get; set; }
        public string Transacao { get; set; }
        public DateTime DataPgto { get; set; }
        public string Status { get; set; }


        public int? SQMATRIC { get; set; }
        public int? CDUOP { get; set; }
        public virtual CLIENTELA Clientela { get; set; }

    }
}
