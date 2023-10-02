using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SESCAP.Ecommerce.Models
{
    
    public class CXDEPRETPDV
    {
        public short TPDEPRET { get; set; }
        public short SQDEPRET { get; set; }
        public string IDUSUARIO { get; set; }
        public decimal VLDEPRET { get; set; }
        public decimal VLENCARGOS { get; set; }
        public DateTime DTDEPRET { get; set; }
        public TimeSpan HRDEPRET { get; set; }
        public short STDEPRET { get; set; }
        public string DSSTATUS { get; set; }
        public int CDMOEDAPGT { get; set; }



        public int NUMCARTAO { get; set; }
        public CARTAO CARTAO { get; set; }

        public int SQCAIXA { get; set; }
        public int CDPESSOA { get; set; }
        public CACAIXA CACAIXA { get; set; }



        public CXDEPRETPDV()
        {
        }




       
    }
}
