using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contta.Services;
using System.Security.Cryptography.X509Certificates;
using Contta.Models;
using Contta.Api.DTOs;
using Contta.Api.Services;
using Contta.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;


namespace Contta.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly JwtTokenService _jwtTokenService;

        public AuthController(UserManager<ApplicationUser> userManager, JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDTO dados)
        {
            var usuario = new ApplicationUser
            {
                NomeCompleto = dados.NomeCompleto,
                Email = dados.Email,
                UserName = dados.Email
            };

            var resultado = await _userManager.CreateAsync(usuario, dados.Senha);

            if (resultado.Succeeded)
            {
                return StatusCode(201, new { mensagem = "Usuário Cadastrado com sucesso!" });
            }
            return BadRequest(resultado.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDTO dados)
        {
            var usuario = await _userManager.FindByEmailAsync(dados.Email);

            if (usuario == null)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var senhaCorreta = await _userManager.CheckPasswordAsync(usuario, dados.Senha);

            if (!senhaCorreta)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var token = _jwtTokenService.GerarToken(usuario);

            return Ok(new {token});
        }
       
    }
}
