using System.Linq;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class PagamentoOnlineRepositorio: IPagamentoOnlineRepositorio
    {
        IConfiguration _conf;
        Db2Context _banco;

        public PagamentoOnlineRepositorio(Db2Context banco, IConfiguration configuration)
        {
            _banco = banco;
            _conf = configuration;
        }

        public void Cadastrar(PagamentoOnline pagamentoOnline)
        {
            _banco.Add(pagamentoOnline);
            _banco.SaveChanges();
        }

        public PagamentoOnline ObterPagamento(int id)
        {
            return _banco.PagamentosOnline.FirstOrDefault(po => po.Id == id);
        }

    }
}
