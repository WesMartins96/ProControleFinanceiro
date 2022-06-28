using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Interfaces;
using ControleFinanceiro.API.Models;

namespace ControleFinanceiro.API.Repositorios
{
    public class CartaoRepositorio : RepositorioGenerico<Cartao>, ICartaoRepositorio
    {

        private readonly Contexto _contexto;
        
        public CartaoRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public IQueryable<Cartao> FiltrarCartoes(string numeroCartao)
        {
            try
            {                                  // filtro pelo numero do cartao
                return _contexto.Cartoes.Where(c => c.Numero.Contains(numeroCartao));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public IQueryable<Cartao> PegarCartoesPeloUsuarioId(string usuarioId)
        {
            try
            {
                return _contexto.Cartoes.Where(c => c.UsuarioId == usuarioId);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}