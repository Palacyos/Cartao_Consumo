using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace SESCAP.Ecommerce.Models
{
    
    public class USUARIO
    {

        public string NMLOGIN { get; set; }
        public string NMSENHA { get; set; }
        public short VBATIVO { get; set; }
        public DateTime DTSENHAEXP { get; set; }
        public short VBSENHAEXP { get; set; }
        public string NMCHAVEAUTENT { get; set; }
        public string IDUSUARIO { get; set; }
        public string NMNICK { get; set; }
        public short VBACESSAPORTAL { get; set; }



        public int CDPESSOA { get; set; }
        public PESSOA PESSOA { get; set; }



        
        public USUARIO()
        {
        }
    }
}
