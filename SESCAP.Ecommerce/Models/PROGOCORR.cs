using System;
using System.Collections;
using System.Collections.Generic;

namespace SESCAP.Ecommerce;

public class PROGOCORR
{
    public int CDPROGRAMA { get; set; }
    public int CDCONFIG { get; set; }
    public int SQOCORRENC { get; set; }
    public string DSUSUARIO { get; set; }
    public DateTime DTLINSCRI { get; set; }
    public DateTime DTUINSCRI { get; set; }
    public DateTime DTLPREINSC { get; set; }
    public DateTime DTUPREINSC { get; set; }
    public DateTime DTLTRANC { get; set; }
    public DateTime DTUTRANC { get; set; }
    public DateTime DTINIOCORR { get; set; }
    public DateTime DTFIMOCORR { get; set; }
    public short VBINSCRUOP { get; set; }
    public string IDUSRRESP { get; set; }
    public int NUVAGAS { get; set; }
    public int NUVAGASOCP { get; set; }
    public int NUMINVOCUP { get; set; }
    public short VBOCORRAPR { get; set; }
    public int CDUOPCAD { get; set; }
    public DateTime DTAPROV { get; set; }
    public DateTime DTATU { get; set; }
    public TimeSpan HRATU { get; set; }
    public string LGATU { get; set; }
    public short DURAULA { get; set; }
    public decimal IDADEMIN { get; set; }
    public decimal IDADEMAX { get; set; }
    public short VBCANCELA { get; set; }
    public string AAMODA { get; set; }
    public int CDMODA { get; set; }
    public short VBBOLBAN { get; set; }
    public DateTime DTINIFER { get; set; }
    public DateTime DTFIMFER { get; set; }
    public string IDTURMASGP { get; set; }
    public string IDVARIAVELTIPO { get; set; }
    public decimal VLMEDIO { get; set; }


    public ICollection<INSCRICAO> INSCRICOES {get; set;}
    

    public PROGOCORR()
    {
        INSCRICOES = new List<INSCRICAO>();
    }

}
