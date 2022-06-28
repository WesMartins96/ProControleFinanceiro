using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFinanceiro.API.Extensions;
using ControleFinanceiro.API.Interfaces;
using ControleFinanceiro.API.Models;
using ControleFinanceiro.API.Repositorios;
using ControleFinanceiro.API.Validacoes;
using ControleFinanceiro.API.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ControleFinanceiro.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

                                               // Usar o SqlServer
            services.AddDbContext<Contexto>(opcoes => opcoes.UseSqlServer(Configuration.GetConnectionString("ConexaoBD")));

            // Expecificar as configurações do Identity
            services.AddIdentity<Usuario, Funcao>().AddEntityFrameworkStores<Contexto>();

            services.ConfigurarSenhaUsuario();

            // Registros das Interfaces e dos Repositorios
            services.AddScoped<ICartaoRepositorio, CartaoRepositorio>();
            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<ITipoRepositorio, TipoRepositorio>();
            services.AddScoped<IFuncaoRepositorio, FuncaoRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IDespesaRepositorio, DespesaRepositorio>();
            services.AddScoped<IMesRepositorio, MesRepositorio>();

            // Registros das classes que nos ajudaram nas validações
            services.AddTransient<IValidator<Categoria>, CategoriaValidator>();
            services.AddTransient<IValidator<FuncoesViewModel>, FuncoesViewModelValidator>();
            services.AddTransient<IValidator<RegistroViewModel>, RegistroViewModelValidator>();
            services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();
            services.AddTransient<IValidator<Cartao>, CartaoValidator>();
            services.AddTransient<IValidator<Despesa>, DespesaValidator>();

            //Fazer a ligação front -> back da aplicação
            services.AddCors();
            


            services.AddSpaStaticFiles(diretorio => {
                diretorio.RootPath = "front/ControleFinanceiro";
            });


             var key = Encoding.ASCII.GetBytes(Settings.ChaveSecreta);

            services.AddAuthentication(opcoes => {
                                                // Para usar o token JWT é necessario baixar o pacote Microsoft.AspNetCore.Authentication.JwtBearer
                                                // Para usar o token JWT é necessario baixar o pacote Microsoft.IdentityModel.Tokens 
                opcoes.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opcoes.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                // Adicionar as informações do Bearer
                .AddJwtBearer(opcoes => {
                    opcoes.RequireHttpsMetadata = false;
                    opcoes.SaveToken = true;
                    //Opcoes de validacoes do Token
                    opcoes.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


                                  //Usando a biblioteca de validações    //Ignorar valores nulos e Ignorar referencias circulares
            services.AddControllers().AddFluentValidation().AddJsonOptions(opcoes => 
            { opcoes.JsonSerializerOptions.IgnoreNullValues = true;})
            .AddNewtonsoftJson(opcoes => {
                opcoes.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControleFinanceiro.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleFinanceiro.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

             //Configurar o CORS
            app.UseCors(opcoes => {
                opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            // Authentication tem que vir sempre antes do Authorization senao, nao funciona
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Configurar a SPA
            app.UseSpa( spa => {
                spa.Options.SourcePath = Path.Combine(Directory.GetCurrentDirectory(), "front/ControleFinanceiro");


                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer($"http://localhost:4200/");
                }
            });
        }
    }
}
