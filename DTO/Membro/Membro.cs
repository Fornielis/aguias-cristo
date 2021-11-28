using System.ComponentModel.DataAnnotations;
using System.Web;
using System;

namespace DTO
{
    public class Membro
    {
        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string CPF { get; set; }
        public string CPFbanco { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Nome { get; set; }

        public string Apelido { get; set; }
        public string Patch { get; set; }
        public string Brasao { get; set; }
        public string Nascimento { get; set; }
        public string Carteirinha { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Sangue { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Unidade { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Estatus { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Anotacao { get; set; }
        public byte[] FotoByte { get; set; }
        public string base64imagem { get; set; }
    }
}
