using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce;

public class COBRANCA
{
    public string IDCLASSE { get; set; }
    public string CDELEMENT { get; set; }
    public int SQCOBRANCA { get; set; }
    public int CDUOPCOB {get; set;}
    public string DSCOBRANCA { get; set; }
    public short RFCOBRANCA { get; set; }
    public decimal VLCOBRADO {get; set;}
    public DateTime DTVENCTO { get; set; }
    public DateTime DTEMISSAO { get; set; }
    public short STRECEBIDO { get; set; }
    public short TPCOBRANCA { get; set; }
    public decimal PCJUROS { get; set; }
    public DateTime DTATU { get; set; }
    public double SMFIELDATU { get; set; }
    public TimeSpan HRATU { get; set; }
    public string LGATU { get; set; }
    public string VLCARACTE1 { get; set; }
    public string VLCARACTE2 { get; set; }
    public short DDCOBJUROS { get; set; }
    public short DDINIJUROS { get; set; }
    public decimal PCMULTA { get; set; }
    public string DSCANCELAM { get; set; }
    public string NMESTACAO { get; set; }
    public int CDCANCELA { get; set; }
    public short TPMORA { get; set; }
    public string LGCANCEL { get; set; }
    public int IDCOBRANCA { get; set; }
    public int IDPEDIDO { get; set; }



    public int CDUOPREC { get; set; }
    public UOP UOP {get; set;}


    public int SQMATRIC { get; set; }
    public int CDUOP { get; set; }
    public CLIENTELA CLIENTELA {get; set;}

    public decimal Juros => PCJUROS/100;
    public decimal Multa => PCMULTA/100;
    
     
    public ICollection<PAGAMENTOS> PAGAMENTOS {get; set;}

    public COBRANCA()
    {
        PAGAMENTOS = new List<PAGAMENTOS>();
    }


}
