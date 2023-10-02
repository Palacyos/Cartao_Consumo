using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace SESCAP.Ecommerce.Models
{
    
    public class CATEGORIA
    {
        public short CDCATEGORI { get; set; }
        public string DSCATEGORI { get; set; }
        public short TPCATEGORI { get; set; }
        public DateTime DTATU { get; set; }
        public string CDIMPRESS { get; set; }
        public TimeSpan HRATU { get; set; }
        public string LGATU { get; set; }
        public short VBCATSERV { get; set; }
        public short VBATIVA { get; set; }
        public short VBPDV { get; set; }
        public short VBEMPOBR { get; set; }
        public short IDCATEGORI { get; set; }
        public short VBCATCONV { get; set; }

        public ICollection<CLIENTELA> CLIENTELAS { get; set; }


        public CATEGORIA()
        {
            CLIENTELAS = new List<CLIENTELA>();
        }

        public string DescTipoCategoria
        {
            get
            {
                if (TPCATEGORI == 0)
                {
                    return "Trabalhador do Comércio";
                }
                else if (TPCATEGORI == 2)
                {
                    return "Público em Geral";
                }
                else
                {
                    return "Dependente";
                }
               
            }
        }

       
    }
}
