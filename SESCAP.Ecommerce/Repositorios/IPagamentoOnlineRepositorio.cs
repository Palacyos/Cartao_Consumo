using System;
using SESCAP.Ecommerce.Models;
using X.PagedList;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface IPagamentoOnlineRepositorio
    {

        void Cadastrar(PagamentoOnline pagamentoOnline); 
        PagamentoOnline ObterPagamento(int Id);
        IPagedList<PagamentoOnline> ObterTodosOsPagamentosDoCliente(int? pagina, int Sqmatric, int Cduop);
    }
}
