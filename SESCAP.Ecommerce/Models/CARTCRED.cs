using System;
namespace SESCAP.Ecommerce.Models
{
	public class CARTCRED
	{
		public decimal QTDPRODCRE { get; set; }
		public decimal VALPRODCRE { get; set; }
		public decimal QTDPRODBLO { get; set; }
		public short VBATIVO { get; set; }
		public decimal VALPRODBLO { get; set; }
		public DateTime DTATU { get; set; }
		public TimeSpan HRATU { get; set; }
		public string LGATU { get; set; }


		public int NUMCARTAO { get; set; }
		public CARTAO CARTAO { get; set; }


		public int CDPRODUTO { get; set; }
        public PRODUTOPDV PRODUTOPDV { get; set; }


        public CARTCRED()
		{
		}
	}
}

