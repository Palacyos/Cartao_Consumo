using System;
using IBM.Data.Db2;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class SaldoCartaoRepositorio : ISaldoCartaoRepositorio
    {
        private Db2Context Banco { get; }
        private IConfiguration Configuration { get; }
        

        public SaldoCartaoRepositorio(Db2Context banco, IConfiguration configuration)
        {
            Banco = banco;
            Configuration = configuration;
            

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

        public void InsereSaldo(int numcartao, int cdproduto, decimal sldvlcart)
        {
            using (var conn = new DB2Connection(Configuration.GetConnectionString("conexaoDb2")))
            {
                string sql = "INSERT INTO SALDOCARTAO (NUMCARTAO, CDPRODUTO, SLDQTCART, SLDQTBLOQ, SLDVLCART, SLDVLBLOQ )" +
                    "VALUES (@NUMCARTAO, @CDPRODUTO, @SLDQTCART, @SLDQTBLOQ, @SLDVLCART, @SLDVLBLOQ)";

                DB2Command cmd = new DB2Command(sql);

                cmd.Connection = conn;

                cmd.Parameters.Add("@NUMCARTAO", numcartao);
                cmd.Parameters.Add("@CDPRODUTO", cdproduto);
                cmd.Parameters.Add("@SLDQTCART", Convert.ToDecimal(0));
                cmd.Parameters.Add("@SLDQTBLOQ", Convert.ToDecimal(0));
                cmd.Parameters.Add("@SLDVLCART", sldvlcart);
                cmd.Parameters.Add("@SLDVLBLOQ", Convert.ToDecimal(0));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
    }
}

