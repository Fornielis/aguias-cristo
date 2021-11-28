using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SistemaErro
    {
        public int IdErro { get; set; }
        public string Usuario { get; set; }
        public string Procedimento { get; set; }
        public string Controller { get; set; }
        public string Acao { get; set; }
        public string Erro { get; set; }
    }
}
