using System;
using System.Collections.Generic;
using System.Linq;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class MoedaPgtoRepositorio : IMoedaPgtoRepositorio
    {

        private Db2Context Banco { get; }

        public MoedaPgtoRepositorio(Db2Context banco)
        {
            Banco = banco;
        }

        public IEnumerable<MOEDAPGTO> ObterMoedasPgto()
        {
            return Banco.MoedaPgtos;
        }
    }
}
