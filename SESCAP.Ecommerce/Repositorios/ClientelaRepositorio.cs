using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
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
            var clientela = Banco.Clientelas.Include(c => c.CARTAO)
                .Include(c => c.UOP)
                .Include(c => c.CATEGORIA)
                .Include(c => c.COBRANCAS.Where(c => c.STRECEBIDO == 0).OrderBy(c => c.DTVENCTO))
                .Include(c => c.INSCRICOES.Where(i => i.STINSCRI == 0 && i.PROGOCORR.DTFIMOCORR > DateTime.Today))
            .FirstOrDefault(c => c.SQMATRIC.Equals(sqmatric) && c.CDUOP.Equals(cduop));

            var dependentes = Banco.Respclis
            .Where(r => r.NUCPF == clientela.NUCPF)
            .Select(r => new {r.SQMATRIC, r.CDUOP})
            .ToList();

            if(dependentes != null && dependentes.Any())
            {
                foreach (var dep in dependentes)
                {
                    var cobrancasDependentes = Banco.Clientelas
                    .Include(c => c.COBRANCAS.Where(c => c.STRECEBIDO == 0).OrderBy(c => c.DTVENCTO))
                    .Include(c => c.INSCRICOES.Where(i => i.STINSCRI == 0 && i.PROGOCORR.DTFIMOCORR > DateTime.Today))
                    .FirstOrDefault(c => c.SQMATRIC == dep.SQMATRIC && c.CDUOP == dep.CDUOP);

                    if (cobrancasDependentes != null)
                    {
                        clientela.COBRANCAS.AddRange(cobrancasDependentes.COBRANCAS);
                        clientela.INSCRICOES.AddRange(cobrancasDependentes.INSCRICOES);
                    }
                }

                clientela.COBRANCAS = clientela.COBRANCAS.OrderBy(c => c.DTVENCTO).ToList();
            }
            return clientela;
        }

        public CLIENTELA ObterCobrancasPorStatus(int sqmatric, int cduop, short status)
        {
            var clientela = Banco.Clientelas
                .Include(c => c.COBRANCAS.Where(c => c.STRECEBIDO == status).OrderByDescending(c => c.DTVENCTO))
                    .ThenInclude(cob => cob.PAGAMENTOS)
            .FirstOrDefault(c => c.SQMATRIC.Equals(sqmatric)&& c.CDUOP.Equals(cduop));

            var dependentes = Banco.Respclis
            .Where(r => r.NUCPF == clientela.NUCPF)
            .Select(r => new {r.SQMATRIC, r.CDUOP})
            .ToList();

            if(dependentes != null && dependentes.Any())
            {
                foreach (var dep in dependentes)
                {
                    var cobrancasDependentes = Banco.Clientelas
                    .Include(c => c.COBRANCAS.Where(c => c.STRECEBIDO == status).OrderByDescending(c => c.DTVENCTO))
                        .ThenInclude(cob => cob.PAGAMENTOS)
                    .FirstOrDefault(c => c.SQMATRIC == dep.SQMATRIC && c.CDUOP == dep.CDUOP);
                    
                    if(cobrancasDependentes != null)
                    {
                        clientela.COBRANCAS.AddRange(cobrancasDependentes.COBRANCAS);
                    }
                }

                clientela.COBRANCAS = clientela.COBRANCAS.OrderByDescending(c => c.DTVENCTO).ToList();
            }
            return clientela;
       }

        public List<CLIENTELA> ObterDependentes(string cpf)
        {
            var dependentes = Banco.Respclis
            .Where(r => r.NUCPF == cpf)
            .Select(r => new {r.SQMATRIC, r.CDUOP})
            .ToList();

            var dependentesClientela = new List<CLIENTELA>();
            
           if(dependentes != null && dependentes.Any())
           {    
                foreach (var dep in dependentes)
                {
                    var cliente = Banco.Clientelas.FirstOrDefault(c => c.SQMATRIC == dep.SQMATRIC && c.CDUOP == dep.CDUOP);

                    if (cliente != null)
                    {
                        dependentesClientela.Add(cliente);
                    }
                }
           }

           return dependentesClientela;
        }
    }
}
