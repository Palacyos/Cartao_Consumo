using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
	public interface ISaldoCartaoRepositorio
	{
        SALDOCARTAO ObterSaldoCartao(int nucartao, int cdproduto);

        void AtualizarSaldoCartao(SALDOCARTAO saldoCartao, decimal sldvlrcartao);
    }
}

