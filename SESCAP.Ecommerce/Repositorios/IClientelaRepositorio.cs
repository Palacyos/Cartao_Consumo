using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface IClientelaRepositorio
    {
        CLIENTELA ValidaMatricula(string matricula, string cpf);

        CLIENTELA ObterClientela(int sqmatric, int cduop);

        CLIENTELA ObterCobrancasPorStatus(int sqmatric, int cduop, short status);
    }
}
