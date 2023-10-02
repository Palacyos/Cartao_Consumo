using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SESCAP.Ecommerce.Models
{
    
    public class MUNICIPIO
    {
        public short CDMUNICIP { get; set; }
        public short? CDMUNINSS { get; set; }
        public string DSMUNICIP { get; set; }
        public string DSMUNMASK { get; set; }
        public DateTime? DTATU { get; set; }
        public TimeSpan? HRATU { get; set; }
        public string LGATU { get; set; }


        public string SIESTADO { get; set; }
        public ESTADO ESTADO { get; set; }


    }
}
