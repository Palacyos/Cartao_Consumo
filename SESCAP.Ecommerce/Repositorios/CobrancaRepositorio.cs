using System;
using System.Linq;
using IBM.Data.Db2;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;

namespace SESCAP.Ecommerce;

public class CobrancaRepositorio : ICobrancaRepositorio
{
    private Db2Context Banco { get; }
    private IConfiguration Configuration { get; }

    public CobrancaRepositorio(Db2Context banco, IConfiguration configuration)
    {   
        Banco = banco;
        Configuration = configuration;
        
    }

    public COBRANCA ObterCobranca(string idClasse, string cdElement, int sqCobranca)
    {
        using(var conn = new DB2Connection(Configuration.GetConnectionString("conexaoDb2")))
        {
            string sql = "SELECT * FROM COBRANCA WHERE IDCLASSE = @IDCLASSE AND CDELEMENT = @CDELEMENT AND SQCOBRANCA = @SQCOBRANCA";

            DB2Command cmd = new DB2Command(sql);
            cmd.Connection = conn;

            cmd.Parameters.Add("@IDCLASSE", idClasse);
            cmd.Parameters.Add("@CDELEMENT", cdElement);
            cmd.Parameters.Add("@SQCOBRANCA", sqCobranca);

            conn.Open();

            using (var reader = cmd.ExecuteReader())
            {
                if(reader.Read())
                {
                    var cobranca = new COBRANCA
                    {
                        IDCLASSE = reader["IDCLASSE"].ToString(),
                        CDELEMENT = reader["CDELEMENT"].ToString(),
                        SQCOBRANCA = Convert.ToInt32(reader["SQCOBRANCA"])
                    };
                    return cobranca;
                }
                else
                {
                    throw new Exception("Cobrança não encontrada.");
                }
            }
        }
    }

    public void AtualizaSituacaoRecebidoCobranca(COBRANCA cobranca)
    { 
        cobranca.STRECEBIDO = Convert.ToInt16(1);
        Banco.Cobrancas.Attach(cobranca);
        Banco.Entry(cobranca).Property(cob => cob.STRECEBIDO).IsModified = true;
        Banco.SaveChanges();

    }

}
