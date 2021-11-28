using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class Evento
    {
        public int IdEvento { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Unidade { get; set; }
        public string Localidade { get; set; }
        public string Endereco { get; set; }
        public string UF { get; set; }
        public string Pais { get; set; }
        public string Contato { get; set; }
        public string Anotacao { get; set; }
        public string Datas { get; set; }
        public string MesDivulgacao { get; set; }
    }
}
