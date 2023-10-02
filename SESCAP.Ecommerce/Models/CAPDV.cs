using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace SESCAP.Ecommerce.Models
{
    
    public class CAPDV
    {
        public int CDPDV { get; set; }
        public string DSPDV { get; set; }
        public string NMESTACAO { get; set; }
        public short VBHOTEL { get; set; }
        public DateTime DTATU { get; set; }
        public TimeSpan HRATU { get; set; }
        public string LGATU { get; set; }
        public short STPDV { get; set; }

        public int CDLOCVENDA { get; set; }
        public LOCALVENDA LOCALVENDA { get; set; }

        public ICollection<CACAIXA> CACAIXAS { get; set; }

        public CAPDV()
        {
            CACAIXAS = new List<CACAIXA>();
        }

    }
}
