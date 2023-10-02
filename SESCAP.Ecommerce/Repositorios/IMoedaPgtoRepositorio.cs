using System;
using System.Collections;
using System.Collections.Generic;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface IMoedaPgtoRepositorio
    {
        IEnumerable<MOEDAPGTO> ObterMoedasPgto();

    }
}
