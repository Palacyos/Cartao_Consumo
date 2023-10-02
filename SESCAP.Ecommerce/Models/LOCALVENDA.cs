using System;
using System.Collections;
using System.Collections.Generic;

namespace SESCAP.Ecommerce.Models
{
	public class LOCALVENDA
	{
		public int CDLOCVENDA { get; set; }
		public string DSLOCVENDA { get; set; }
		public DateTime DTATU { get; set; }
		public TimeSpan HRATU { get; set; }
		public string LGATU { get; set; }
		public string MSGCUPOM { get; set; }
		public short STLOCVENDA { get; set; }


		public int CDUOP { get; set; }
		public UOP UOP { get; set; }

		public ICollection<CAPDV> CAPDVS { get; set; }

		public LOCALVENDA()
		{
			CAPDVS = new List<CAPDV>();
		}
	}
}

