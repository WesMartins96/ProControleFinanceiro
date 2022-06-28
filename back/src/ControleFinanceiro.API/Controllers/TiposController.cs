using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Interfaces;
using ControleFinanceiro.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TiposController : ControllerBase
    {
        private readonly ITipoRepositorio _tipoRepositorio;

        public TiposController(ITipoRepositorio tipoRepositorio)
        {
            _tipoRepositorio = tipoRepositorio;
        }

        //Pegar dados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo>>> GetTipos()
        {
            return await _tipoRepositorio.PegarTodos().ToListAsync();
        }

    }
}