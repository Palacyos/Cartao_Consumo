using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;
using System.Data;
using IBM.Data.Db2;

namespace SESCAP.Ecommerce.Repositorios
{
    public class HstMovCartRepositorio: IHstMovCartReposiotrio
    {
       
        Db2Context Banco { get;}
        IConfiguration Configuration { get; }

        public HstMovCartRepositorio(Db2Context banco, IConfiguration configuration)
        {
            Banco = banco;
            Configuration = configuration;

        }

        public void InsereMovCartaoConsumo(int cdproduto, DateTime dtmoviment, int nucartao, decimal vlrprodmov, int sqcaixa, int cdpessoa)
        {
            HSTMOVCART hst = new HSTMOVCART();

            TimeSpan hrnow = DateTime.Now.TimeOfDay;

            var ultimaSequenciaHstMovimento = Banco.Hstmovcarts.Where(hst => hst.CDPRODUTO.Equals(cdproduto) && hst.DTMOVIMENT.Equals(dtmoviment) && hst.NUMCARTAO.Equals(nucartao)).ToList().LastOrDefault();

            var ultimoIdHisMovCart = Banco.Hstmovcarts.OrderBy(hst => hst.DTMOVIMENT).LastOrDefault();

            using (var conn = new DB2Connection(Configuration.GetConnectionString("conexaoDb2")))
            {

                string sql = "INSERT INTO HSTMOVCART (CDPRODUTO, DTMOVIMENT, NUMCARTAO, SQMOVIMENT, VBCREDEB, HRMOVIMENT, TPMOVIMENT, QTDPRODMOV, VLPRODMOV, OBSMOVIMEN, DTATU, HRATU, LGATU, SQCAIXA, IDCHECKOUT, IDHSTMOVCART, CDPESSOA, IDUSUARIO)" +
                    "VALUES (@CDPRODUTO, @DTMOVIMENT, @NUMCARTAO, @SQMOVIMENT, @VBCREDEB, @HRMOVIMENT, @TPMOVIMENT, @QTDPRODMOV, @VLPRODMOV, @OBSMOVIMEN, @DTATU, @HRATU, @LGATU, @SQCAIXA, @IDCHECKOUT, @IDHSTMOVCART, @CDPESSOA, @IDUSUARIO)";

                DB2Command cmd = new DB2Command(sql);

                cmd.Connection = conn;

                cmd.Parameters.Add("@CDPRODUTO", cdproduto);
                cmd.Parameters.Add("@DTMOVIMENT", dtmoviment);
                cmd.Parameters.Add("@NUMCARTAO", nucartao);

                if (ultimaSequenciaHstMovimento == null)
                {
                    cmd.Parameters.Add("@SQMOVIMENT", Convert.ToInt16(hst.SQMOVIMENT+1));
                }
                else
                {
                    cmd.Parameters.Add("@SQMOVIMENT", Convert.ToInt16(ultimaSequenciaHstMovimento.SQMOVIMENT + 1));
                }
                cmd.Parameters.Add("@VBCREDEB", Convert.ToInt16(0));
                cmd.Parameters.Add("@HRMOVIMENT", hrnow);
                cmd.Parameters.Add("@TPMOVIMENT", Convert.ToInt16(0));
                cmd.Parameters.Add("@QTDPRODMOV", Convert.ToDecimal(0));
                cmd.Parameters.Add("@VLPRODMOV", vlrprodmov);
                cmd.Parameters.Add("@OBSMOVIMEN", Configuration.GetValue<string>("ObsMovimento"));
                cmd.Parameters.Add("@DTATU", dtmoviment);
                cmd.Parameters.Add("@HRATU", hrnow);
                cmd.Parameters.Add("@LGATU", cdpessoa.ToString());
                cmd.Parameters.Add("@SQCAIXA", sqcaixa);
                cmd.Parameters.Add("@IDCHECKOUT", Convert.ToInt16(0));
                cmd.Parameters.Add("@IDHSTMOVCART", ultimoIdHisMovCart.IDHSTMOVCART + 1);
                cmd.Parameters.Add("@CDPESSOA", cdpessoa);
                cmd.Parameters.Add("@IDUSUARIO", null);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }

        }
    }
}
