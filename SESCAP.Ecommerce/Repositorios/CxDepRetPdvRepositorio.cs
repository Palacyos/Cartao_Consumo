using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class CxDepRetPdvRepositorio: ICxDepRetPdvRepositorio
    {
        private Db2Context Banco { get; }
        IConfiguration Configuration { get; }

        public CxDepRetPdvRepositorio(Db2Context banco, IConfiguration configuration)
        {
            Banco = banco;
            Configuration = configuration;
        }

        public CXDEPRETPDV CadastraDeposito(int nuCartao, int sqcaixa, int cdpessoa, decimal vlrDeposito, int moedaPgto)
        {
            DateTime dataAtual = DateTime.Now.Date;
            TimeSpan horaAtual = DateTime.Now.TimeOfDay;
            var ultimoCaixaDepositoPessoa = Banco.Cxdepretpdvs.Where(dpt => dpt.SQCAIXA.Equals(sqcaixa) && dpt.CDPESSOA.Equals(cdpessoa)).ToList().LastOrDefault();

            CXDEPRETPDV deposito = new CXDEPRETPDV();
            deposito.SQCAIXA = sqcaixa ;


            if (ultimoCaixaDepositoPessoa == null)
            {
                deposito.SQDEPRET += Convert.ToInt16(deposito.SQDEPRET + 1);
            }
            else
            {
                deposito.SQDEPRET += Convert.ToInt16(ultimoCaixaDepositoPessoa.SQDEPRET + 1);
            }
            deposito.VLDEPRET = vlrDeposito;
            deposito.DTDEPRET = dataAtual;
            deposito.HRDEPRET = horaAtual;
            deposito.DSSTATUS = Configuration.GetValue<string>("DsStatusCxDepRetPdv");
            deposito.CDMOEDAPGT = moedaPgto;
            deposito.NUMCARTAO = nuCartao;
            deposito.CDPESSOA = cdpessoa;
            

            Banco.Add(deposito);
            Banco.SaveChanges();

            return deposito;
        }
    }
}
