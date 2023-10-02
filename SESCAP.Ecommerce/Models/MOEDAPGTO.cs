using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace SESCAP.Ecommerce.Models
{
    
    public class MOEDAPGTO
    {
        public int CDMOEDAPGT { get; set; }
        public string DSMOEDAPGT { get; set; }
        public DateTime DTATU { get; set; }
        public TimeSpan HRATU { get; set; }
        public string LGATU { get; set; }
        public short VBOBS { get; set; }
        public string OBSRECIBO { get; set; }
        public string IDOBS { get; set; }
        public short VBTPCONC { get; set; }
        public short TPCONTRL { get; set; }
        public short VBATIVO { get; set; }
        public short GRCONTA_AR { get; set; }
        public string CDCONTA_AR { get; set; }
        public int MAREFINI_AR { get; set; }
        public int NUELEM_AR { get; set; }



        public MOEDAPGTO()
        {
        }
    }
}
