using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class Contato
    {
        public int IdContato { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Email { get; set; }
        public string Mensagem { get; set; }
    }
}
