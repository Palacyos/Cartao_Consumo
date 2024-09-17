using System;
using System.Collections;
using System.Collections.Generic;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce;

public class PROGRAMAS
{
    public int CDPROGRAMA { get; set; }
    public string NMPROGRAMA { get; set; }
    public string TECONTEUDO { get; set; }
    public short VBINSCRI { get; set; }
    public string DSDURACAO { get; set; }
    public string DSPERIODO { get; set; }
    public int CDPROGSUP { get; set; }
    public DateTime DTATU { get; set; }
    public TimeSpan HRATU { get; set; }
    public string LGATU { get; set; }
    public short STATUS { get; set; }
    
    
    public int CDUOP { get; set; }
    public UOP UOP{get; set;}
    

    public ICollection<PROGSUBMOD> PROGSUBMODS {get; set;}

    public PROGRAMAS()
    {
        PROGSUBMODS = new List<PROGSUBMOD>();
    }
}
