using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Interfaces;
using ControleFinanceiro.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesesController : Controller
    {
        private readonly IMesRepositorio _mesRepositorio;

        public MesesController(IMesRepositorio mesRepositorio)
        {
            _mesRepositorio = mesRepositorio;
        }

        [HttpGet]
        public async Task<IEnumerable<Mes>> GetMeses()
        {
            return await _mesRepositorio.PegarTodos().ToListAsync();
        }


    }
}