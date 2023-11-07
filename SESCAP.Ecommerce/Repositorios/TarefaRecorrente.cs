using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class TarefaRecorrente : ITarefaRecorrente
	{
        private Db2Context Banco { get; }
        private IConfiguration Configuration { get; }

        public TarefaRecorrente(Db2Context banco, IConfiguration configuration)
		{
            Banco = banco;
            Configuration = configuration;

		}

        TimeSpan horaAtual = DateTime.Now.TimeOfDay;
        DateTime data = DateTime.Now;


        public void FechaCaixa()
        {

            var caixa = Banco.Cacaixas.Where(cx => cx.CDPESSOA == Configuration.GetValue<int>("CdPessoa") && cx.DTFECHAMEN == null && cx.STCAIXA == 0 && cx.CDPDV == Configuration.GetValue<int>("CAPDV")).ToList().LastOrDefault();

           
            if (caixa != null)
            {

                caixa.DTFECHAMEN = data;
                caixa.HRFECHAMEN = horaAtual;
                caixa.SMFIELDATU = 312;
                caixa.STCAIXA = 1;
                caixa.LGFECHAMEN = caixa.CDPESSOA.ToString();

                Banco.Cacaixas.Attach(caixa);
                Banco.Entry(caixa).Property(cx => cx.DTFECHAMEN).IsModified = true;
                Banco.Entry(caixa).Property(cx => cx.HRFECHAMEN).IsModified = true;
                Banco.Entry(caixa).Property(cx => cx.SMFIELDATU).IsModified = true;
                Banco.Entry(caixa).Property(cx => cx.STCAIXA).IsModified = true;
                Banco.Entry(caixa).Property(cx => cx.LGFECHAMEN).IsModified = true;

                CAIXALANCA lanc = new CAIXALANCA();

                lanc.IDUSUARIO = null;
                lanc.SQCAIXA = caixa.SQCAIXA;
                lanc.SQLANCAMEN = 1;
                lanc.TPLANCAMEN = 0;
                lanc.IDUSRLANCA = Configuration.GetValue<int>("CdPessoa").ToString();
                lanc.DTLANCAMEN = data;
                lanc.HRLANCAMEN = horaAtual;
                lanc.DSLANCAMEN = Configuration.GetValue<string>("DsLancamento");
                lanc.VLLANCAMEN = Convert.ToDecimal(caixa.VLSALDOATU);
                lanc.STLANCAMEN = 1;
                lanc.DSSTATUS = null;
                lanc.CDPESSOA = Configuration.GetValue<int>("CdPessoa");

                caixa.VLSALDOATU = 0;
                Banco.Entry(caixa).Property(cx => cx.VLSALDOATU).IsModified = true;


                Banco.Add(lanc);
                Banco.SaveChanges();

            }
        }

       
    }
}

