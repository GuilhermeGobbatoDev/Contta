using Contta.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Contta.Api.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuracao;
        public JwtTokenService(IConfiguration configuration)
        {
            _configuracao = configuration;
        }

        public string GerarToken(ApplicationUser usuario)
        {
            var chave = _configuracao["Jwt:Key"] ?? throw new InvalidOperationException("Chave JWT não configurada.");

            var chaveSeguranca = new SymmetricSecurityKey(Convert.FromBase64String(chave));

            var credenciais = new SigningCredentials(chaveSeguranca,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credenciais,
                issuer: "Contta",
                audience: "Contta.Api"
             );

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
