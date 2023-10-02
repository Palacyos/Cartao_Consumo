using System;
using System.Collections.Generic;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface IHstMovCartReposiotrio
    {
       
        void InsereMovCartaoConsumo(int cdproduto, DateTime dtmoviment, int nucartao, decimal vlrprodmov, int sqcaixa, int cdpessoa);

    }
}
