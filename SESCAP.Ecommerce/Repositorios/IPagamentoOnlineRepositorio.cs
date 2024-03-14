using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface IPagamentoOnlineRepositorio
    {

        void Cadastrar(PagamentoOnline pagamentoOnline); 
        PagamentoOnline ObterPagamento(int Id);
    }
}
