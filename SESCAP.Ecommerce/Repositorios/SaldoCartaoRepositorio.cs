using System;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class SaldoCartaoRepositorio: ISaldoCartaoRepositorio
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

        public void InsereValorRecarga(int nuCartao,int cdproduto, decimal sldvlrcartao)
        {
            SALDOCARTAO saldoCartao = new SALDOCARTAO();
            saldoCartao.NUMCARTAO = nuCartao;
            saldoCartao.CDPRODUTO = cdproduto;
            saldoCartao.SLDQTCART = 0;
            saldoCartao.SLDQTBLOQ = 0;
            saldoCartao.SLDVLCART = sldvlrcartao;
            saldoCartao.SLDVLBLOQ = 0;

            Banco.Add(saldoCartao);
            Banco.SaveChanges();
        }

        public void AtualizarSaldoCartao(SALDOCARTAO saldoCartao, decimal sldvlrcartao)
        {
            saldoCartao.SLDVLCART += sldvlrcartao;
            Banco.Saldos.Attach(saldoCartao);
            Banco.Entry(saldoCartao).Property(s => s.SLDVLCART).IsModified = true;
            Banco.SaveChanges();
        }
    }
}
