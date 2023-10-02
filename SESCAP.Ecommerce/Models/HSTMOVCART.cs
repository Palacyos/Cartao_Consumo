using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SESCAP.Ecommerce.Models
{
    
    public class HSTMOVCART
    {
        public DateTime DTMOVIMENT { get; set; }
        public short SQMOVIMENT { get; set; }
        public short VBCREDEB { get; set; }
        public TimeSpan HRMOVIMENT { get; set; }
        public short TPMOVIMENT { get; set; }
        public decimal QTDPRODMOV { get; set; }
        public decimal VLPRODMOV { get; set; }
        public string OBSMOVIMEN { get; set; }
        public DateTime DTATU { get; set; }
        public TimeSpan HRATU { get; set; }
        public string LGATU { get; set; }
        public short IDCHECKOUT { get; set; }
        public int IDHSTMOVCART { get; set; }
        public string IDUSUARIO { get; set; }


        public int SQCAIXA { get; set; }
        public int? CDPESSOA { get; set; }
        public CACAIXA CACAIXA { get; set; }



        public int NUMCARTAO { get; set; }
        public CARTAO CARTAO { get; set; }



        public int CDPRODUTO { get; set; }
        public PRODUTOPDV PRODUTOPDV { get; set; }



        public string DescVbCreDeb
        {
            get
            {
                return VBCREDEB == 0 ? "Creditado" : "Débito";
            }
        }



    }
}
