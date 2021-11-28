using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "DIGITE SEU USUÁRIO")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "FORMATO DATA = dd/mm/aaaa")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "DIGITE SUA SENHA")]
        public string Senha { get; set; }
    }
}
