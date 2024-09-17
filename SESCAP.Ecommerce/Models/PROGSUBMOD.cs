using System;

namespace SESCAP.Ecommerce;

public class PROGSUBMOD
{
    public int ANOPROG { get; set; }
    public DateTime DTATU { get; set; }
    public TimeSpan HRATU { get; set; }
    public string LGATU { get; set; }
    public short CDDEBFOL { get; set; }
    public short GRCONTA_AR { get; set; }
    public string CDCONTA_AR { get; set; }
    public int MAREFINI_AR { get; set; }


    public int CDPROGRAMA { get; set; }
    public PROGRAMAS PROGRAMAS {get; set;}


    public short CDADMIN { get; set; }
    public short CDMAPA { get; set; }
    public short CDREALIZAC { get; set; }
    public short CDMODALIDA { get; set; }
    public short CDSUBMODAL { get; set; }
    public SGP_SUBMODALID SGP_SUBMODALID {get; set;}
    

}
