using System;

namespace SESCAP.Ecommerce.Models
{
    public class ClientelaCobrancaViewModel
    {
        public string DsCobranca { get; set; }
        public DateTime DtVencto { get; set; }
        public decimal Valor { get; set; }
        public string CdElement { get; set; }
        public short StRecebido { get; set; }
        public string IdClasse {get; set;}
        public int Sqcobranca {get; set;}
        public decimal ValorDesconto {get; set;}
        public decimal ValorJuros {get; set;}

    }
}