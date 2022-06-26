using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ControleFinanceiro.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace ControleFinanceiro.API.Services
{

    // classe responsavel por gerar o token do ususario
    public static class TokenService
    {
        public static string GerarToken(Usuario usuario, string funcaoUsuario){
            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Settings.ChaveSecreta);
            var tokenDescriptor = new SecurityTokenDescriptor
            {   
                            // ClaimsIdentity -> da pra criar um conjuto de informacoes para identificar algo, q no caso é o usuario, identificar o usuario
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
                    new Claim(ClaimTypes.Role, funcaoUsuario)
                }),
                // quando o token vai expirar
                Expires = DateTime.UtcNow.AddHours(2),                                                     //algoritmo de encriptação
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //serilializar o token no formato Jwt e retornalo
            return tokenHandler.WriteToken(token);
        }
    }
}