﻿using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
	public interface ICartCredRepositorio
	{

		CARTCRED ObterCartaoCredito(int numCartao, int cdProduto);

		void AtualizarValorProdutoCredito(CARTCRED cartaoCredito, decimal valorProdutoCredito, DateTime dataAtualizacao, TimeSpan horaAtualizacao, string loginAtualizacao);

	}
}

