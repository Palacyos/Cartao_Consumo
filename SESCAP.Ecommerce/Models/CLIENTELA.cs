using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SESCAP.Ecommerce.Models
{
    public class CLIENTELA
    {
        
        public int SQMATRIC { get; set; }
        public string CDCLASSIF { get; set; }
        public short NUDV { get; set; }
        public string NUCGCCEI { get; set; }
        public short? CDNIVEL { get; set; }
        public int? SQTITULMAT { get; set; }
        public int? CDUOTITUL { get; set; }
        public short STMATRIC { get; set; }
        public DateTime DTINSCRI { get; set; }
        public string CDMATRIANT { get; set; }
        public DateTime DTVENCTO { get; set; }
        public string NMCLIENTE { get; set; }
        public DateTime DTNASCIMEN { get; set; }
        public string NMPAI { get; set; }
        public string CDSEXO { get; set; }
        public string NMMAE { get; set; }
        public short CDESTCIVIL { get; set; }
        public short VBESTUDANT { get; set; }
        public short? NUULTSERIE { get; set; }
        public string DSNATURAL { get; set; }
        public string DSNACIONAL { get; set; }
        public short? NUDEPEND { get; set; }
        public string NUCTPS { get; set; }
        public DateTime? DTADMISSAO { get; set; }
        public DateTime? DTDEMISSAO { get; set; }
        public string NUREGGERAL { get; set; }
        public decimal? VLRENDA { get; set; }
        public string NUCPF { get; set; }
        public string NUPISPASEP { get; set; }
        public string DSCARGO { get; set; }
        public DateTime? DTEMIRG { get; set; }
        public string IDORGEMIRG { get; set; }
        public short? DSPARENTSC { get; set; }
        public byte[] FOTO { get; set; }
        public DateTime DTATU { get; set; }
        public short? STEMICART { get; set; }
        public TimeSpan HRATU { get; set; }
        public string LGATU { get; set; }
        public double SMFIELDATU { get; set; }
        public string TEOBS { get; set; }
        public int NRVIACART { get; set; }
        public string PSWCLI { get; set; }
        public string PSWCRIP { get; set; }
        public decimal? VLRENDAFAM { get; set; }
        public string NMSOCIAL { get; set; }
        public short SITUPROF { get; set; }
        public short TIPOIDENTIDADE { get; set; }
        public string COMPIDENTIDADE { get; set; }
        public short VBPCG { get; set; }
        public short VBEMANCIPADO { get; set; }
        public short VBPCD { get; set; }
        public string IDNACIONAL { get; set; }
        public short STONLINE { get; set; }
        public short VBNOMEAFETIVO { get; set; }

        
        public int CDUOP { get; set; }
        public UOP UOP { get; set; }

        
        public short CDCATEGORI { get; set; }
        public CATEGORIA CATEGORIA { get; set; }

        
        public int? NUMCARTAO { get; set; }
        public CARTAO CARTAO { get; set; }


        public virtual ICollection<PagamentoOnline> PagamentosOnline { get; set; }


        public string MatFormat => $"{CDUOP.ToString().PadLeft(4, '0')}-{SQMATRIC.ToString().PadLeft(6, '0')}-{NUDV}";

        /*
		* -> propriedade que carrega a foto da base
		*/
        public string CarregaFoto
        {
            get
            {
                if (FOTO != null)
                {
                    string foto = Convert.ToBase64String(FOTO);
                    return string.Format("data:image/png;base64,{0}", foto);
                }

                return null;
            }
        }

        public CLIENTELA()
        {

        }
    }
}
