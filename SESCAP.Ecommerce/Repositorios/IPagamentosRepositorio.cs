using System;

namespace SESCAP.Ecommerce;

public interface IPagamentosRepositorio
{
    PAGAMENTOS CadastraPagamento(int sqCaixa, int cdMoedapgto,
    string idClasse, string cdElement, int sqCobranca, decimal vlrRecebido, decimal vlrJuros, string dsPagamento, decimal vlrDesconto, int cdUopPgto, int cdPessoa );
}
