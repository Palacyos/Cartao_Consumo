using System;
using IBM.Data.DB2.Core;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class CartCredRepositorio : ICartCredRepositorio
	{

        private Db2Context Banco { get; }
        private IConfiguration Configuration { get; }

        public CartCredRepositorio(Db2Context banco, IConfiguration configuration)
		{
            Banco = banco;
            Configuration = configuration;
		}

        public CARTCRED ObterCartaoCredito(int numCartao, int cdProduto)
        {
            return Banco.CartCreds.Find(numCartao, cdProduto);
        }

        public void AtualizarValorProdutoCredito(CARTCRED cartaoCredito, decimal valorProdutoCredito, DateTime dataAtualizacao, TimeSpan horaAtualizacao, string loginAtualizacao)
        {
            cartaoCredito.VALPRODCRE += valorProdutoCredito;
            cartaoCredito.DTATU = dataAtualizacao;
            cartaoCredito.HRATU = horaAtualizacao;
            cartaoCredito.LGATU = loginAtualizacao;

            Banco.Update(cartaoCredito);
            Banco.SaveChanges();
        }

        public void InsereValorProdutoCredito(int numcartao, int cdproduto, decimal valprodcre, DateTime dataAtualizacao, TimeSpan horaAtualizacao, string loginAtualizacao)
        {
            using (var conn = new DB2Connection(Configuration.GetConnectionString("conexaoDb2")))
            {
                string sql = "INSERT INTO CARTCRED (NUMCARTAO, CDPRODUTO, QTDPRODCRE, VALPRODCRE, QTDPRODBLO, VBATIVO, VALPRODBLO, DTATU, HRATU, LGATU )" +
                    "VALUES (@NUMCARTAO, @CDPRODUTO, @QTDPRODCRE, @VALPRODCRE, @QTDPRODBLO, @VBATIVO, @VALPRODBLO, @DTATU, @HRATU, @LGATU)";

                DB2Command cmd = new DB2Command(sql);

                cmd.Connection = conn;

                cmd.Parameters.Add("@NUMCARTAO", numcartao);
                cmd.Parameters.Add("@CDPRODUTO", cdproduto);
                cmd.Parameters.Add("@QTDPRODCRE", Convert.ToDecimal(0));
                cmd.Parameters.Add("@VALPRODCRE", valprodcre);
                cmd.Parameters.Add("@QTDPRODBLO", Convert.ToDecimal(0));
                cmd.Parameters.Add("@VBATIVO", Convert.ToInt16(1));
                cmd.Parameters.Add("@VALPRODBLO", Convert.ToDecimal(0));
                cmd.Parameters.Add("@DTATU", dataAtualizacao);
                cmd.Parameters.Add("@HRATU", horaAtualizacao);
                cmd.Parameters.Add("@LGATU", loginAtualizacao);


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }
    }
}

