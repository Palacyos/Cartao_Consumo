using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface IProdutoPdvRepositorio
    {

        PRODUTOPDV ObterProdutoPdv(int cdproduto);

    }
}
