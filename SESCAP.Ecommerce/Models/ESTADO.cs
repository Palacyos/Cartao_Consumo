using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SESCAP.Ecommerce.Models
{
    
    public class ESTADO
    {
        public string SIESTADO { get; set; }
        public string CDESTADO { get; set; }
        public short CDREGIAO { get; set; }
        public string DSESTADO { get; set; }
        public string CDINSS { get; set; }
        public string DSUFMASK { get; set; }
        public string LGATU { get; set; }
        public DateTime? DTATU { get; set; }
        public TimeSpan? HRATU { get; set; }
        public short? CDMUNICIP { get; set; }


        public ADMIN ADMIN { get; set; }

        public ICollection<MUNICIPIO> MUNICIPIOS { get; set; }

        public ESTADO()
        {
            MUNICIPIOS = new List<MUNICIPIO>();
        }

    }
}
