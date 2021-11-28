using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Unidade
    {
        public int IdUnidade { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Estatus { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string UF { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Cidade { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string UrlSite { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string UrlGoogleMaps { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Reunioes { get; set; }
    }
}
