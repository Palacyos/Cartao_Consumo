using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SESCAP.Ecommerce.Libraries.GeradorQRCode;

namespace SESCAP.Ecommerce.Models
{
    
    public class CARTAO
    {

        public int NUMCARTAO { get; set; }
        public short TPCONTRL { get; set; }
        public string PSWCART { get; set; }
        public DateTime DTATU { get; set; }
        public TimeSpan HRATU { get; set; }
        public string LGATU { get; set; }
        public DateTime DTCADASTRO { get; set; }
        public string CDBARRA { get; set; }
        public string NUCHIP { get; set; }
        public short TPCARTAO { get; set; }
        public short STCARTAO { get; set; }

        public CLIENTELA CLIENTELA { get; set; }

        public CARTCRED CARTCRED { get; set; }

        public SALDOCARTAO SALDOCARTAO { get; set; }

        public ICollection<HSTMOVCART> HSTMOVCARTS { get; set; }
        public ICollection<CXDEPRETPDV> CXDEPRETPDVs { get; set; }

        public CARTAO()
        {
            HSTMOVCARTS = new List<HSTMOVCART>();
        }

        public string QrCode
        {
            get
            {
               return GeradorQrCode.GerarImagem(CDBARRA);
            }
        }
 
    }
}
