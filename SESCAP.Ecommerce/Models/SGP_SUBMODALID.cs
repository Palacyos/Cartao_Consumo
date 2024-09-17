using System;
using System.Collections.Generic;

namespace SESCAP.Ecommerce;

public class SGP_SUBMODALID
{
    public short CDADMIN { get; set; }
    public short CDMAPA { get; set; }
    public short CDREALIZAC { get; set; }
    public short CDMODALIDA { get; set; }
    public short CDSUBMODAL { get; set; }
    public string DSSUBMODAL { get; set; }
    public DateTime DTVALIDADE { get; set; }
    public DateTime DTINICIO { get; set; }
    public string IDSGP { get; set; }


    public ICollection<PROGSUBMOD> PROGSUBMODS {get; set;}


    public SGP_SUBMODALID()
    {
        PROGSUBMODS = new List<PROGSUBMOD>();
    }
}
