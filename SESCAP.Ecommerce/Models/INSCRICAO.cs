using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce;

public class INSCRICAO
{
    public int CDDESCONTO { get; set; }
    public int CDFONTEINF { get; set; }
    public int CDFORMATO { get; set; }
    public int CDPERFIL { get; set; }
    public short STINSCRI { get; set; }
    public DateTime DTPREINSCR { get; set; }
    public DateTime DTINSCRI { get; set; }
    public string LGINSCRI { get; set; }
    public DateTime DTPRIVENCT { get; set; }
    public short NUCOBRANCA { get; set; }
    public short CDUOPINSC { get; set; }
    public DateTime DTSTATUS { get; set; }
    public TimeSpan HRSTATUS { get; set; }
    public string LGSTATUS { get; set; }
    public short CDUOPSTAT { get; set; }
    public string DSSTATUS { get; set; }
    public short STCANCELAD { get; set; }
    
    

    public int CDPROGRAMA { get; set; }
    public int CDCONFIG { get; set; }
    public int SQOCORRENC { get; set; }
    public PROGOCORR PROGOCORR { get; set; }


    public int SQMATRIC { get; set; }
    public int CDUOP { get; set; }
    public CLIENTELA CLIENTELA {get; set;}
    

    public short CDCATEGORI { get; set; }
    public CATEGORIA CATEGORIA {get; set;}

}
