using System;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
	public class SaldoCartaoRepositorio : ISaldoCartaoRepositorio
    {
        private Db2Context Banco { get; }


        public SaldoCartaoRepositorio(Db2Context banco)
        {
            Banco = banco;

        }

        public SALDOCARTAO ObterSaldoCartao(int nucartao, int cdproduto)
        {
            return Banco.Saldos.Find(nucartao, cdproduto);
        }

        public void AtualizarSaldoCartao(SALDOCARTAO saldoCartao, decimal sldvlrcartao)
        {
            saldoCartao.SLDVLCART += sldvlrcartao;
            Banco.Update(saldoCartao);
            Banco.SaveChanges();
        }
    }
}

