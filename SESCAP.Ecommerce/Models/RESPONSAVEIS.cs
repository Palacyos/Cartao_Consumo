using System;
using System.Collections.Generic;

namespace SESCAP.Ecommerce;

public class RESPONSAVEIS
{
    public short VBATIVO { get; set; } 
    public string NUCPF { get; set; }
    public string NUREGGERAL { get; set; }
    public DateTime DTEMIRG { get; set; }
    public string IDORGEMIRG { get; set; }
    public string NMRESPONSA { get; set; }
    public DateTime DTATU { get; set; }
    public TimeSpan HRATU { get; set; }
    public string LGATU { get; set; }
    

    public ICollection<RESPCLI> RESPCLIs { get; set; }

    public RESPONSAVEIS()
    {
        RESPCLIs = new List<RESPCLI>();
    }

}
