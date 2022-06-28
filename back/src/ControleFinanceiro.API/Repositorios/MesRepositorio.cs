using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Interfaces;
using ControleFinanceiro.API.Models;

namespace ControleFinanceiro.API.Repositorios
{

    /* essa classe nao terá metodo nenhum porque todos os metodos que eu preciso
     está em RepositorioGenerico */

    public class MesRepositorio : RepositorioGenerico<Mes>, IMesRepositorio
    {
        public MesRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}