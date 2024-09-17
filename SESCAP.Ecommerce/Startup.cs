using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using IBM.EntityFrameworkCore;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Repositorios;
using SESCAP.Ecommerce.Libraries.Sessao;
using SESCAP.Ecommerce.Libraries.Login;
using System.Globalization;
using SESCAP.Ecommerce.Libraries.Pagamento.Cielo;
using SESCAP.Ecommerce.Libraries.Midleware;
using System.Net.Mail;
using System.Net;
using SESCAP.Ecommerce.Libraries.Email;
using AutoMapper;
using SESCAP.Ecommerce.Libraries.AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using System;
using Hangfire.InMemory;
using Microsoft.Extensions.Logging;

namespace SESCAP.Ecommerce
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
       

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }


        public void ConfigureServices(IServiceCollection services)
        {
           

            services.AddHangfire(config =>
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseInMemoryStorage());

            services.AddHangfireServer();


            var cultureInfo = new CultureInfo("pt-BR");
            cultureInfo.NumberFormat.CurrencySymbol = "R$";
            

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            services.AddControllersWithViews();

            /*
             * -> configuração da conexão com o Banco de Dados IBM DB2 
             */
            services.AddDbContext<Db2Context>(options => options.UseDb2(Configuration.GetConnectionString("conexaoDb2"),
            p => p.SetServerInfo(IBMDBServerType.LUW, IBMDBServerVersion.LUW_11_01_1010)));




            /*
             * -> padrão resposiório
             */
            services.AddHttpContextAccessor();
            services.AddScoped<ICadastroLoginRepositorio, CadastroLoginRepositorio>();
            services.AddScoped<IClientelaRepositorio, ClientelaRepositorio>();
            services.AddScoped<ICartaoRepositorio, CartaoRepositorio>();
            services.AddScoped<ICacaixaRepositorio, CacaixaRepositorio>();
            services.AddScoped<ICapdvRepositorio, CapdvRepositorio>();
            services.AddScoped<IMoedaPgtoRepositorio, MoedaPgtoRepositorio>();
            services.AddScoped<IPagamentoOnlineRepositorio, PagamentoOnlineRepositorio>();
            services.AddScoped<ICxDepRetPdvRepositorio, CxDepRetPdvRepositorio>();
            services.AddScoped<IProdutoPdvRepositorio, ProdutoPdvRepositorio>();
            services.AddScoped<ISaldoCartaoRepositorio, SaldoCartaoRepositorio>();
            services.AddScoped<IHstMovCartReposiotrio, HstMovCartRepositorio>();
            services.AddScoped<ICartCredRepositorio, CartCredRepositorio>();
            services.AddScoped<ITarefaRecorrente, TarefaRecorrente>();
            services.AddScoped<IPagamentosRepositorio, PagamentosRepositorio>();
            services.AddScoped<ICobrancaRepositorio, CobrancaRepositorio>();
            
           

            /*
             * -> SMTP 
            */
            services.AddScoped(options => {

                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true

                };
    
                return smtp;
            });

            /*
             * -> Session - configuração - memoryCache -> guarda os dados na memória
            */
            services.AddMemoryCache();  
            services.AddHttpContextAccessor();
            services.AddSession(op => { });
            services.AddScoped<Sessao>();
            services.AddScoped<LoginClientela>();
            services.AddScoped<GerenciarCielo>();
            services.AddScoped<GerenciarEmail>();

            var mappingConfig = new MapperConfiguration(mc => {

                mc.AddProfile(new MappingProfile());

            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            /*
             * -> Rota Personalizada
            */
            services.AddRouting(op => op.ConstraintMap["slugify"] = typeof(RouteSlugifyParameterTransformer));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJoManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/{0}");
                app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
                
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller:slugify=Home}/{action:slugify=Login}/{id?}"
                );
            });

            app.UseHangfireDashboard();
            recurringJoManager.AddOrUpdate("Fecha Caixa", () => serviceProvider.GetService<ITarefaRecorrente>().FechaCaixa(), Cron.Daily(2));
           

        }
    }
}
