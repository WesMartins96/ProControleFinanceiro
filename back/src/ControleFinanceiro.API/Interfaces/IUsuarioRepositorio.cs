using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Models;
using Microsoft.AspNetCore.Identity;

namespace ControleFinanceiro.API.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        // Retorna a quantidade de usuario que temos
        Task<int> PegarQuatidadeUsuariosRegistrados();

        // Criação de usuario
        Task<IdentityResult> CriarUsuario(Usuario usuario, string senha);

        Task IncluirUsuarioEmFuncao(Usuario usuario, string funcao);

        //Função para logar o usuario
                                    //Bool que será usado para lembrar ou não do usuario
        Task LogarUsuario(Usuario usuario, bool lembrar);

        Task<Usuario> PegarUsuarioPeloEmail(string email);

        Task<IList<string>> PegarFuncoesUsuario(Usuario usuario);


    }
}