using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface ICxDepRetPdvRepositorio
    {
       
        CXDEPRETPDV CadastraDeposito(int nuCartao, int sqcaixa, int cdpessoa, DateTime dtDeposito, decimal vlrDeposito, int moedaPgto);

    }
}
