using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class ClientelaRepositorio : IClientelaRepositorio
    {

        private Db2Context Banco { get; }

        public ClientelaRepositorio(Db2Context banco)
        {
            Banco = banco;
        }

        public CLIENTELA ValidaMatricula(string matricula, string cpf)
        {
            int cduop = int.Parse(matricula.Split('-')[0]);
            int sqmatric = int.Parse(matricula.Split('-')[1]);
            short nudv = short.Parse(matricula.Split('-')[2]);
            string nucpf = cpf.Replace(".", "").Replace("-", "");

            return Banco.Clientelas.FirstOrDefault(c => c.CDUOP.Equals(cduop) && c.SQMATRIC.Equals(sqmatric) && c.NUDV.Equals(nudv) && c.NUCPF.Equals(nucpf));
        }

        public CLIENTELA ObterClientela(int sqmatric, int cduop)
        {
            return Banco.Clientelas.Include(c => c.CARTAO)
                .Include(c => c.UOP)
                .Include(c => c.CATEGORIA)
                .Include(c => c.COBRANCAS.Where(c => c.STRECEBIDO == 0).OrderBy(c => c.DTVENCTO))
                .Include(c => c.INSCRICOES.Where(i => i.STINSCRI == 0 && i.PROGOCORR.DTFIMOCORR > DateTime.Today))
            .FirstOrDefault(c => c.SQMATRIC.Equals(sqmatric) && c.CDUOP.Equals(cduop));
        }

        public CLIENTELA ObterCobrancasPorStatus(int sqmatric, int cduop, short status)
        {
            return Banco.Clientelas
                .Include(c => c.COBRANCAS.Where(c => c.STRECEBIDO == status).OrderByDescending(c => c.DTVENCTO))
                    .ThenInclude(cob => cob.PAGAMENTOS)
            .FirstOrDefault(c => c.SQMATRIC.Equals(sqmatric)&& c.CDUOP.Equals(cduop));
       }
    }
}
