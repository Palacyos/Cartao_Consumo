using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce;

public class RESPCLI
{
    public DateTime DTATU { get; set; }
    public TimeSpan HRATU { get; set; }
    public string LGATU { get; set; }



    public string NUCPF { get; set; }
    public RESPONSAVEIS RESPONSAVEIS { get; set; }    


    public int SQMATRIC { get; set; }
    public int CDUOP { get; set; }
    public CLIENTELA CLIENTELA{get; set;}
}
