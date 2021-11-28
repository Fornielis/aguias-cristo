using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "DIGITE SEU USUÁRIO")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "DIGITE SUA SENHA")]
        public string Senha { get; set; }
        public int tentativas { get; set; }
    }
}
