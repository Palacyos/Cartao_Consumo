using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SESCAP.Ecommerce.Models
{
   
    public class ADMIN
    {
        
        public short CDADMIN { get; set; }
        public short? NUORDEMANU { get; set; }
        public string SIADMIN { get; set; }
        public string NMADMIN { get; set; }
        public DateTime DTATU { get; set; }
        public TimeSpan HRATU { get; set; }
        public DateTime? DTENDOMKT { get; set; }
        public TimeSpan? HRENDOMKT { get; set; }
        public string LGATU { get; set; }
        public string NUCNPJ { get; set; }

       
        public string SIESTADO { get; set; }
        public ESTADO ESTADO { get; set; }

        
        public ICollection<PESSOA> PESSOAS { get; set; }
        public ICollection<UOP> UOPS { get; set; }

        public ADMIN()
        {
            PESSOAS = new List<PESSOA>();
        }

        
        public string DescRegional
        {
            get
            {
                if (CDADMIN == 7)
                {
                    return "Sesc Amapá";

                }
                else {

                    return NMADMIN;
                }
            }
        }

    }
}
