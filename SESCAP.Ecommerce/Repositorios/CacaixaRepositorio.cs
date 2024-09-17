using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;
using Microsoft.Extensions.Logging;
using IBM.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace SESCAP.Ecommerce.Repositorios
{
    public class CacaixaRepositorio : ICacaixaRepositorio
    {

        private Db2Context Banco { get; }
        private IConfiguration Configuration { get; }

        public CacaixaRepositorio(Db2Context banco, IConfiguration configuration)
        {   
            Banco = banco;
            Configuration = configuration;
           
        }

        public CACAIXA ObterCaixaAberto(int cdpdv)
        {       
            return Banco.Cacaixas.FirstOrDefault(cx => cx.DTFECHAMEN == null && cx.STCAIXA == 0 && cx.CDPDV == cdpdv && cx.CDPESSOA.Equals(Configuration.GetValue<int>("CdPessoa")));
        }

        public CACAIXA CaixaDeposito(int sqcaixa, int cdPessoa)
        {
            return Banco.Cacaixas
                .Include(cx => cx.PESSOA)
                    .ThenInclude(p => p.USUARIO)
            .FirstOrDefault(cx => cx.SQCAIXA.Equals(sqcaixa) && cx.CDPESSOA.Equals(cdPessoa));
        }
      
        public CACAIXA AbreCaixa(int cdpdv, int cdpessoa, int cduop, string nmestacao, int cdlocvenda)
        {
            var dataAtual =  DateTime.Now;
            var horaAtual = DateTime.Now.TimeOfDay;
            
            var ultimoCaixaAbertoPessoa = Banco.Cacaixas.Where(c => c.CDPESSOA.Equals(cdpessoa)).ToList().LastOrDefault();

            var anoFechamento = dataAtual.ToString("yyyy");
            var numero = CACAIXA.NuFechamento(anoFechamento);


            CACAIXA ca = new CACAIXA();
            ca.IDUSUARIO = null;

            if (ultimoCaixaAbertoPessoa == null)
            {
                ca.SQCAIXA += ca.SQCAIXA+1;
            }
            else
            {
                ca.SQCAIXA += ultimoCaixaAbertoPessoa.SQCAIXA + 1;
            }
            ca.DTABERTURA = Convert.ToDateTime(dataAtual);

            if (ultimoCaixaAbertoPessoa == null)
            {
                ca.NUFECHAMEN = numero + 1;
            }
            else
            {
                var nuFechamento = CACAIXA.FormatNuFechamento(ultimoCaixaAbertoPessoa.NUFECHAMEN);
                if (dataAtual.Year.Equals(nuFechamento))
                {
                    ca.NUFECHAMEN += ultimoCaixaAbertoPessoa.NUFECHAMEN + 1;
                }
                else
                {
                    ca.NUFECHAMEN = numero + 1;
                }
            }
            ca.HRABERTURA = horaAtual;
            ca.DTFECHAMEN = null;
            ca.HRFECHAMEN = null;
            ca.STCAIXA = 0;
            ca.VLSALDOANT = 0;
            ca.VLSALDOATU = 0;
            ca.SMFIELDATU = 0;
            ca.CDPDV = cdpdv;
            ca.CDLOCVENDA = cdlocvenda;
            ca.CDUOP = cduop;
            ca.LGFECHAMEN = null;
            ca.NMESTACAO = nmestacao;
            ca.CDPESSOA = cdpessoa;
            ca.DTAUTORIZ = null;
            ca.CDUNIDADE = null;
            ca.NUAUTORIZ = null;
            ca.TPAUTORIZ = null;

            Banco.Add(ca);
            Banco.SaveChanges();

            return ca;

        }

        public void AtualizaSaldoCaixa(CACAIXA caixa, decimal vlsaldoatu)
        {

            caixa.VLSALDOATU += vlsaldoatu;
            Banco.Cacaixas.Attach(caixa);
            Banco.Entry(caixa).Property(cx => cx.VLSALDOATU).IsModified = true;
            Banco.SaveChanges();
        }
    }
}
