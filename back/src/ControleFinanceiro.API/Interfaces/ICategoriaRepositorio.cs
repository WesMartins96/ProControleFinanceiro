using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Models;

namespace ControleFinanceiro.API.Interfaces
{
    public interface ICategoriaRepositorio : IRepositorioGenerico<Categoria>
    {
        new IQueryable<Categoria> PegarTodos();

        new Task<Categoria> PegarPeloId(int id);
    }
}