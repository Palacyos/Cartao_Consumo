using System;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class CartCredRepositorio : ICartCredRepositorio
	{

        private Db2Context Banco { get; }

        public CartCredRepositorio(Db2Context banco)
		{
            Banco = banco;
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

    }
}

