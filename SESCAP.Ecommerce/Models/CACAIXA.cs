using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SESCAP.Ecommerce.Models
{
    
    public class CACAIXA
    {
        public string IDUSUARIO { get; set; }
        public int SQCAIXA { get; set; }
        public DateTime DTABERTURA { get; set; }
        public int NUFECHAMEN { get; set; }
        public TimeSpan HRABERTURA { get; set; }
        public DateTime? DTFECHAMEN { get; set; }
        public TimeSpan? HRFECHAMEN { get; set; }
        public short STCAIXA { get; set; }
        public decimal VLSALDOANT { get; set; }
        public decimal? VLSALDOATU { get; set; }
        public double SMFIELDATU { get; set; }
        public int? CDLOCVENDA { get; set; }
        public string LGFECHAMEN { get; set; }
        public string NMESTACAO { get; set; }
        public DateTime? DTAUTORIZ { get; set; }
        public short? CDUNIDADE { get; set; }
        public short? NUAUTORIZ { get; set; }
        public string TPAUTORIZ { get; set; }

       
        public int CDUOP { get; set; }
        public UOP UOP { get; set; }


        public int CDPESSOA { get; set; }
        public PESSOA PESSOA { get; set; }


        public int? CDPDV { get; set; }
        public CAPDV CAPDV { get; set; }


        public ICollection<HSTMOVCART> HSTMOVCARTS { get; set; }
        public ICollection<CXDEPRETPDV> CXDEPRETPDVs { get; set; }
        public ICollection<CAIXALANCA> LANCAMENTOSCAIXA { get; set; }

        public string NumeroFechamentoFormatado
        {
            get
            {
                return TransformaNumeroFechamento(NUFECHAMEN);
            }
        }

        public CACAIXA()
        {
            HSTMOVCARTS = new List<HSTMOVCART>();
            CXDEPRETPDVs = new List<CXDEPRETPDV>();
            LANCAMENTOSCAIXA = new List<CAIXALANCA>();
        }


        public static int FormatNuFechamento(int valor)
        {
            int startIndex = 0;
            int length = 4;

            string valorString = valor.ToString();

            string substring = valorString.Substring(startIndex, length);

            int valorInt = int.Parse(substring);

            return valorInt;

        }

        public static int NuFechamento(string conteudo)
        {
           
            var numero = int.Parse($"{conteudo}0000");
            return numero;
            
        }

        public string TransformaNumeroFechamento(int numeroFechamento)
        {
            string entradaString = numeroFechamento.ToString();
           
            string ano = entradaString[..4];
            
            string fechamentoNumero = entradaString.Substring(4, 4);

            string numeroFechamentoFormatado = $"{fechamentoNumero}/{ano}";

            string numfechamento = numeroFechamentoFormatado.TrimStart('0');

            return numfechamento;
        }
    }
}
