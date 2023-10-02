using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface ISaldoCartaoRepositorio
    {
        SALDOCARTAO ObterSaldoCartao(int nucartao, int cdproduto);

        void InsereValorRecarga(int nuCartao,int cdproduto, decimal sldvlrcartao);

        void AtualizarSaldoCartao(SALDOCARTAO saldoCartao, decimal sldvlrcartao);
    }
}
