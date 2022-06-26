using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFinanceiro.API.Extensions
{
    public static class ConfiguracaoIdentityExtension
    {
        public static void ConfigurarSenhaUsuario(this IServiceCollection services)
        {
            // Configuração da senha
            services.Configure<IdentityOptions>(opcoes => {
                opcoes.Password.RequireDigit = false;
                opcoes.Password.RequireLowercase = false;
                opcoes.Password.RequiredLength = 6; 
                opcoes.Password.RequireNonAlphanumeric = false;
                opcoes.Password.RequiredUniqueChars = 0;
            });
        }
    }
}