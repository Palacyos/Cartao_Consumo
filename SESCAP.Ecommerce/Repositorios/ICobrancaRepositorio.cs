using System;

namespace SESCAP.Ecommerce;

public interface ICobrancaRepositorio
{
    void AtualizaSituacaoRecebidoCobranca(COBRANCA cobranca);

    COBRANCA ObterCobranca(string idClasse, string cdElement, int sqCobranca);
}
