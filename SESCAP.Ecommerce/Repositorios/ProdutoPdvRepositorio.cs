using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class ProdutoPdvRepositorio: IProdutoPdvRepositorio
    {
        private Db2Context Banco { get; }

        public ProdutoPdvRepositorio(Db2Context banco)
        {
            Banco = banco;
        }

        public PRODUTOPDV ObterProdutoPdv(int cdproduto)
        {
            return Banco.ProdutoPdvs.Find(cdproduto);
        }
    }
}
