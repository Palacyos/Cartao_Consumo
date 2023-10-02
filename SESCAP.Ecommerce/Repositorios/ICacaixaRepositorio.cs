using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface ICacaixaRepositorio
    {

        CACAIXA ObterCaixaAberto(int cdpdv);

        CACAIXA CaixaDeposito(int sqcaixa, int cdPessoa);

        CACAIXA AbreCaixa(int cdpdv, int cdpessoa, int cduop, string nmestacao, int cdlocvenda);

        void AtualizaSaldoCaixa(CACAIXA caixa, decimal vlsaldoatu);
    }
}
