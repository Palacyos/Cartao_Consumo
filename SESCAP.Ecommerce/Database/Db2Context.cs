using Microsoft.EntityFrameworkCore;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Database
{
    public class Db2Context : DbContext
    {


        public DbSet<CLIENTELA> Clientelas { get; set; }
        public DbSet<UOP> Uops { get; set; }
        public DbSet<CARTAO> Cartoes { get; set; }
        public DbSet<CATEGORIA> Categorias { get; set; }
        public DbSet<CadastroLoginSescAP> Cadastros { get; set; }
        public DbSet<HSTMOVCART> Hstmovcarts { get; set; }
        public DbSet<PRODUTOPDV> ProdutoPdvs { get; set; }
        public DbSet<CACAIXA> Cacaixas { get; set; }
        public DbSet<CAPDV> Capdvs { get; set; }
        public DbSet<PESSOA> Pessoas { get; set; }
        public DbSet<ESTADO> Estados { get; set; }
        public DbSet<MUNICIPIO> Municipios { get; set; }
        public DbSet<ADMIN> Admins { get; set; }
        public DbSet<SALDOCARTAO> Saldos { get; set; }
        public DbSet<USUARIO> Usuarios { get; set; }
        public DbSet<CXDEPRETPDV> Cxdepretpdvs { get; set; }
        public DbSet<MOEDAPGTO> MoedaPgtos { get; set; }
        public DbSet<PagamentoOnline> PagamentosOnline { get; set; }
        public DbSet<LOCALVENDA> LocalVendas { get; set; }
        public DbSet<CAIXALANCA> LancamentosCaixas { get; set; }
        public DbSet<CARTCRED> CartCreds { get; set; }

        public Db2Context(DbContextOptions<Db2Context> options) : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

            modelbuilder.ApplyConfiguration(new ClientelaConfiguracao());
            modelbuilder.ApplyConfiguration(new UopConfiguracao());
            modelbuilder.ApplyConfiguration(new CategoriaConfiguracao());
            modelbuilder.ApplyConfiguration(new CartaoConfiguracao());
            modelbuilder.ApplyConfiguration(new CadastroLoginSescAPConfiguracao());
            modelbuilder.ApplyConfiguration(new ProdutoPdvConfiguracao());
            modelbuilder.ApplyConfiguration(new CapdvConfiguracao());
            modelbuilder.ApplyConfiguration(new PessoaConfiguracao());
            modelbuilder.ApplyConfiguration(new CacaixaConfiguracao());
            modelbuilder.ApplyConfiguration(new HstmovcartConfiguracao());
            modelbuilder.ApplyConfiguration(new AdminConfiguracao());
            modelbuilder.ApplyConfiguration(new EstadoConfiguracao());
            modelbuilder.ApplyConfiguration(new MunicipioConfiguracao());
            modelbuilder.ApplyConfiguration(new SaldocartaoConfiguracao());
            modelbuilder.ApplyConfiguration(new UsuarioConfiguracao());
            modelbuilder.ApplyConfiguration(new CxDepRetPdvConfiguracao());
            modelbuilder.ApplyConfiguration(new MoedaPgtoConfiguracao());
            modelbuilder.ApplyConfiguration(new PagamentoOnlineConfiguracao());
            modelbuilder.ApplyConfiguration(new LocalVendaConfiguracao());
            modelbuilder.ApplyConfiguration(new CaixaLancaConfiguracao());
            modelbuilder.ApplyConfiguration(new CartCredConfiguracao());
            
            
        }

    }
}
