using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce;

public class PAGAMENTOS
{
    public string IDUSUARIO { get; set; }
    public DateTime DTRECEBIDO { get; set; }
    public TimeSpan HRRECEBIDO { get; set; }
    public int CDMOEDAPGT { get; set; }
    public int SQPAGAMENT { get; set; }
    public decimal VLRECEBIDO { get; set; }
    public decimal VLJUROS { get; set; }
    public double SMFIELDATU { get; set; }
    public short STCANCELAD { get; set; }
    public string DSCANCELAD {get; set;}
    public decimal VLACRESCIM { get; set; }
    public decimal VLDESCONTO { get; set; }
    public short NUIMPVIA2 { get; set; }
    public int CDUOPPGTO { get; set; }
    public int? SQVENDA { get; set; }
    public string LGCANCEL { get; set; }
    public DateTime? DTCANCEL { get; set; }
    public TimeSpan? HRCANCEL { get; set; }
    public string NMESTACAO { get; set; }
    public int? IDCOBRANCA { get; set; }


    public string IDCLASSE { get; set; }
    public string CDELEMENT { get; set; }
    public int SQCOBRANCA { get; set; }
    public COBRANCA COBRANCA {get; set;}


    public int CDPESSOA { get; set; }
    public PESSOA PESSOA {get; set;}

    public int SQCAIXA { get; set; }
    public CACAIXA CACAIXA {get; set;}
}
