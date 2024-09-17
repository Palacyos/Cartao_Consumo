using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SESCAP.Ecommerce.Models
{
    
    public class PESSOA
    {
        public string NMPESSOA { get; set; }
        public int CDPESSOA { get; set; }
        public DateTime? DTCADASTRO { get; set; }
        public DateTime DTATU { get; set; }
        public TimeSpan HRATU { get; set; }
        public short TPPESSOA { get; set; }
        public string LGATU { get; set; }


        public USUARIO USUARIO { get; set; }

        public ICollection<CACAIXA> CACAIXAS { get; set; }
        public ICollection<CAIXALANCA> LANCAMENTOSCAIXA { get; set; }
        public ICollection<PAGAMENTOS> PAGAMENTOS {get; set;}


        public short? CDADMIN { get; set; }
        public ADMIN ADMIN { get; set; }

        public PESSOA()
        {
            CACAIXAS = new List<CACAIXA>();
            LANCAMENTOSCAIXA = new List<CAIXALANCA>();
            PAGAMENTOS = new List<PAGAMENTOS>();
        }

    }
}
