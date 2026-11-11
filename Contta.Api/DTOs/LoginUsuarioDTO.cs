using System.ComponentModel.DataAnnotations;

namespace Contta.Api.DTOs
{
    public class LoginUsuarioDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Senha { get; set; } = string.Empty;
    }
}
