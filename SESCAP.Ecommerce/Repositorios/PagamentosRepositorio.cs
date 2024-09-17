using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios;

public class PagamentosRepositorio : IPagamentosRepositorio
{
    private readonly Db2Context _banco; 
    private readonly IConfiguration _configuration; 

    public PagamentosRepositorio(Db2Context banco, IConfiguration configuration)
    {
        _banco = banco;
        _configuration = configuration;        
    }

    public PAGAMENTOS CadastraPagamento(int sqCaixa, int cdMoedapgto, string idClasse, string cdElement, int sqCobranca, decimal vlrRecebido, decimal vlrJuros, string dsPagamento,
    decimal vlrDesconto, int cdUopPgto, int cdPessoa)
    {
        DateTime dataAtual = DateTime.Now.Date;
        TimeSpan horaAtual = DateTime.Now.TimeOfDay;

        var ultimoPagamentoPessoa = _banco.Pagamentos.Where(p => p.SQCAIXA.Equals(sqCaixa) && p.CDPESSOA.Equals(cdPessoa)).ToList().LastOrDefault();

        PAGAMENTOS pg = new PAGAMENTOS();
        pg.DTRECEBIDO = dataAtual;
        pg.SQCAIXA = sqCaixa;
        pg.HRRECEBIDO = horaAtual;
        pg.CDMOEDAPGT = cdMoedapgto;
        if(ultimoPagamentoPessoa == null)
        {
            pg.SQPAGAMENT += pg.SQPAGAMENT + 1;
        }
        else
        {
            pg.SQPAGAMENT = ultimoPagamentoPessoa.SQPAGAMENT + 1;
        }
        pg.IDCLASSE = idClasse;
        pg.CDELEMENT = cdElement;
        pg.SQCOBRANCA = sqCobranca;
        pg.VLRECEBIDO = vlrRecebido;
        pg.VLJUROS = vlrJuros;
        pg.SMFIELDATU = 0;
        pg.STCANCELAD = 0;
        pg.DSCANCELAD = _configuration.GetValue<string>("DsPgamento");
        pg.VLACRESCIM = 0;
        pg.VLDESCONTO = vlrDesconto;
        pg.NUIMPVIA2 = 0;
        pg.CDUOPPGTO = cdUopPgto;
        pg.CDPESSOA = cdPessoa;

        _banco.Add(pg);
        _banco.SaveChanges();

        return pg;

    }
}
