using System;
namespace SESCAP.Ecommerce.Models
{
	public class SALDOCARTAO
	{

        public decimal SLDQTCART { get; set; }
        public decimal SLDQTBLOQ { get; set; }
        public decimal SLDVLCART { get; set; }
        public decimal SLDVLBLOQ { get; set; }

        public int NUMCARTAO { get; set; }
        public CARTAO CARTAO { get; set; }

        public int CDPRODUTO { get; set; }
        public PRODUTOPDV PRODUTOPDV { get; set; }

        public SALDOCARTAO()
		{
		}




	}
}

