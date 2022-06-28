using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Interfaces;
using ControleFinanceiro.API.Models;
using ControleFinanceiro.API.Services;
using ControleFinanceiro.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
      
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            var usuario = await _usuarioRepositorio.PegarPeloId(id);

            if(usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        //Salvar foto do usuario
        [HttpPost("SalvarFoto")]
        public async Task<IActionResult> SalvarFoto()
        {
            //pegar foto do Formulario
            var foto = Request.Form.Files[0];
            byte[] b;

            using (var openReadStream = foto.OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await openReadStream.CopyToAsync(memoryStream);
                    b = memoryStream.ToArray();
                }
            }

            return Ok(new
            {
                foto = b
            });
        }

        // Registrar usuario
        [HttpPost("RegistrarUsuario")]
        public async Task<ActionResult> RegistrarUsuario(RegistroViewModel model)
        {
            // Verificar se os dados são validos
            if (ModelState.IsValid)
            {
                // Para ter o status de criação do usuario
                IdentityResult usuarioCriado;
                string funcaoUsuario;

                Usuario usuario = new Usuario
                {
                    UserName = model.NomeUsuario,
                    Email = model.Email,
                    PasswordHash = model.Senha,
                    CPF = model.CPF,
                    Profissao = model.Profissao,
                    Foto = model.Foto
                };


                // o primeiro usuario sempre será o admin
                if(await _usuarioRepositorio.PegarQuatidadeUsuariosRegistrados() > 0)
                {
                    funcaoUsuario = "Usuario";
                }
                else
                {
                    funcaoUsuario = "Administrador";
                }

                usuarioCriado = await _usuarioRepositorio.CriarUsuario(usuario, model.Senha);

                if (usuarioCriado.Succeeded)
                {
                    await _usuarioRepositorio.IncluirUsuarioEmFuncao(usuario, funcaoUsuario);
                    var token = TokenService.GerarToken(usuario, funcaoUsuario);
                    await _usuarioRepositorio.LogarUsuario(usuario, false);

                    return Ok(new
                    {
                        emailUsuarioLogado = usuario.Email,
                        usuarioId = usuario.Id,
                        tokenUsuarioLogado = token
                    });
                }
                // se nao for criado com sucesso
                else
                {
                    return BadRequest(model);
                }
            }

            return BadRequest(model);
        }


        //Login
        [HttpPost("LogarUsuario")]
        public async Task<ActionResult> LogarUsuario(LoginViewModel model)
        {
            if (model == null)          
                return NotFound("usuario e / ou senhas inválidos");

            Usuario usuario = await _usuarioRepositorio.PegarUsuarioPeloEmail(model.Email);    

            if (usuario != null)
            {
                // password hasher para decripitar a senha do nosso usuario
                PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();
                // verificar a senha do usuario                                                      
                if (passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) != PasswordVerificationResult.Failed)
                {
                    var funcoesUsuario = await _usuarioRepositorio.PegarFuncoesUsuario(usuario);
                    // criar o token
                    var token  = TokenService.GerarToken(usuario, funcoesUsuario.First());
                    await _usuarioRepositorio.LogarUsuario(usuario, false);

                    return Ok(new {
                        emailUsuarioLogado = usuario.Email,
                        usuarioId = usuario.Id,
                        tokenUsuarioLogado = token
                    });
                }
                // se a senha nao bater
                return NotFound("Usuário e / ou senha inválidos");
            }


            // caso o usuario nao for encontrado
            return NotFound("Usuário e / ou senha inválidos");
        }



    }
}