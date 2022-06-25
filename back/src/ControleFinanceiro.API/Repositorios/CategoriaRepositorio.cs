using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Interfaces;
using ControleFinanceiro.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.API.Repositorios
{
    public class CategoriaRepositorio : RepositorioGenerico<Categoria>, ICategoriaRepositorio
    {
        private readonly Contexto _contexto;

        public CategoriaRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public new IQueryable<Categoria> PegarTodos()
        {
            try
            {
                return _contexto.Categorias.Include(c => c.Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new async Task<Categoria> PegarPeloId(int id)
        {
            try
            {
                var entity = await _contexto.Categorias.Include(c => c.Tipo).FirstOrDefaultAsync(c => c.CategoriaId == id);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Categoria> FiltrarCategorias(string nomeCategoria)
        {
            try
            {
                var entity = _contexto.Categorias.Include(c => c.Tipo).Where(c => c.Nome.Contains(nomeCategoria));
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}