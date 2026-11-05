using System.ComponentModel.DataAnnotations;
namespace Contta.Api.DTOs
{
    public class RegistrarUsuarioDTO
    {
        [Required]
        [MinLength(3)]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Senha { get; set; } = string.Empty;
    }
}
