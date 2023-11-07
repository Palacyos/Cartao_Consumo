using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
	public interface ISaldoCartaoRepositorio
	{
        SALDOCARTAO ObterSaldoCartao(int nucartao, int cdproduto);

        void InsereSaldo(int numcartao,int cdproduto, decimal sldvlcart);

        void AtualizarSaldoCartao(SALDOCARTAO saldoCartao, decimal sldvlrcartao);
    }
}

