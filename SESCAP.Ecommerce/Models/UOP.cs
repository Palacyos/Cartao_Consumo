using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SESCAP.Ecommerce.Models
{
    
    public class UOP
    {
        public int CDUOP { get; set; }
        public string NMUOP { get; set; }
        public string NUCGCUOP { get; set; }
        public TimeSpan HRATU { get; set; }
        public DateTime DTATU { get; set; }
        public string LGATU { get; set; }
        public string NUCGCANT { get; set; }
        public short VBDR { get; set; }
        public short VBCOLFER { get; set; }
        public int? NUELEM_AR { get; set; }
        public string AAMODA_AR { get; set; }
        public int? CDMODA_AR { get; set; }
        public short STUOP { get; set; }
        public short STREALIZAMATRICULA { get; set; }

        public short CDADMIN { get; set; }
        public ADMIN ADMIN { get; set; }


        public ICollection<CLIENTELA> CLIENTELAS { get; set; }
        public ICollection<CACAIXA> CACAIXAS { get; set; }
        public ICollection<LOCALVENDA> LOCALVENDAS { get; set; }
        public ICollection<PROGRAMAS> PROGRAMAS {get; set;}
        public ICollection<COBRANCA> COBRANCAS {get; set;}

        public string CnpjFormat => $"{NUCGCUOP[..2]}.{NUCGCUOP.Substring(2,3)}.{NUCGCUOP.Substring(5,3)}/{NUCGCUOP.Substring(8,4)}-{NUCGCUOP.Substring(12,2)}";

        public UOP()
        {
            CLIENTELAS = new List<CLIENTELA>();
            CACAIXAS = new List<CACAIXA>();
            LOCALVENDAS = new List<LOCALVENDA>();
            PROGRAMAS = new List<PROGRAMAS>();
            COBRANCAS = new List<COBRANCA>();
        }
    }
}
